using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Common.Types;
using S3.Common.Utility;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;
using S3.Services.Record.Utility;

namespace S3.Services.Record.ClassReports.Queries
{
    public class GetClassReportQueryHandler : IQueryHandler<GetClassReportQuery, ClassReport>
    {
        private readonly RecordDbContext _db;

        public GetClassReportQueryHandler(RecordDbContext db)
            => (_db) = (db);

        public async Task<ClassReport> HandleAsync(GetClassReportQuery query)
        {
            // 1.   Populate the ScoreGrade look up table

            var scoreGrades = new List<ScoreGrade>();
            IQueryable<StudentScore> set = _db.StudentScores.AsNoTracking();

            set = query.SchoolId is null ?
                set : set.Where(x => x.SchoolId == query.SchoolId);

            set = set.Where(x => x.Session == query.Session);

            List<StudentScore> availableScores = set.ToList();
            if (!availableScores.Any())
                return new ClassReport(); // return an empty result

            List<StudentScore> classScores;
            List<StudentScore> reportScores;

            if (query.StudentId is null) // Generating report for the whole class i.e. ClassId was specified
            {
                classScores = availableScores.Where(x => x.ClassId == query.ClassId).ToList();
                reportScores = classScores;
            }
            else // Generating report for a specific student i.e. ClassId was not specified
            {
                reportScores = availableScores.Where(x => x.StudentId == query.StudentId).ToList();
                classScores = availableScores.Where(x => x.ClassId == reportScores.First().ClassId).ToList();
            }

            // Get the Rules to be used in computing weighted scores and grades
            var ruleIds = new HashSet<Guid>(classScores.Select(x => x.RuleId).Distinct());
            List<Rule> rules = new List<Rule>(); 
           
            foreach (var id in ruleIds)
            {
                var rule = await _db.Rules.FirstOrDefaultAsync(x => x.Id == id);
                rules.Add(rule);
            }

            var terms = new HashSet<string>(classScores.Select(x => x.Term).Distinct());
            var subjects = new HashSet<string>(classScores.Select(x => x.Subject).Distinct());

            foreach (var term in terms)
            {
                var termScores = classScores.Where(x => x.Term == term);

                foreach (var subject in subjects)
                {
                    var subjectScores = termScores.Where(x => x.Subject == subject);
                    var studentIds = new HashSet<Guid>(subjectScores.Select(x => x.StudentId));

                    foreach (var id in studentIds)
                    {
                        // Get all scors for this student in subjectScores
                        var studentScores = subjectScores.Where(x => x.StudentId == id);
                        (int weightedScore, char grade) = GetWeightedScoreAndGrade(studentScores, rules.First(x => x.Id == studentScores.First().RuleId));

                        scoreGrades.Add(new ScoreGrade
                        {
                            Term = term,
                            Subject = subject,
                            StudentId = id,
                            WeightedScore = weightedScore,
                            Grade = grade
                        });
                    }
                }
            }


            // 2.   Generate Score Statistics for each subject offered in each term

            var scoresStat = new ScoresStat
            {
                TermStats = new List<TermStat>()
            };

            foreach (var term in terms)
            {
                var termStat = new TermStat
                { 
                    Term = term,
                    Stats = new List<Stat>()
                };

                foreach (var subject in subjects)
                {
                    var stat = new Stat { Subject = subject };
                    var scores = scoreGrades.Where(x => x.Term == term && x.Subject == subject);

                    if (scores.Any())
                    {
                        stat.ClassMinScore = scores.Min(x => x.WeightedScore);
                        stat.ClassMaxScore = scores.Max(x => x.WeightedScore);
                        stat.ClassAverageScore = (int) Math.Round(scores.Average(x => x.WeightedScore));

                       termStat.Stats.Add(stat);
                    }
                }
                scoresStat.TermStats.Add(termStat);
            }


            // 3.   Generate Report Card information

            var classReport = new ClassReport 
            { 
                ScoresStat = scoresStat,
                StudentReports = new List<StudentReport>()
            };
            var _studentIds = new HashSet<Guid>(reportScores.Select(x => x.StudentId));

            foreach (var id in _studentIds)
            {
                var studentScores = reportScores.Where(x => x.StudentId == id);
                var studentReport = new StudentReport 
                { 
                    StudentName = studentScores.First().StudentName,
                    TermReports = new List<TermReport>()
                };

                var _terms = new HashSet<string>(studentScores.Select(x => x.Term)); // Use a different terms variable because a student may not have result for one or two terms

                foreach (var term in _terms)
                {
                    var termScores = studentScores.Where(x => x.Term == term);
                    var termReport = new TermReport
                    {
                        Term = termScores.First().Term,
                        SubjectReports = new List<SubjectReport>()
                    };

                    var __subjects = new HashSet<string>(termScores.Select(x => x.Subject));

                    foreach (var subject in __subjects)
                    {
                        var subjectScores = termScores.Where(x => x.Subject == subject);
                        var subjectReport = new SubjectReport
                        {
                            Subject = subject,
                            WeightedScore = scoreGrades.FirstOrDefault(x => x.StudentId == id && x.Term == term && x.Subject == subject)?.WeightedScore,
                            Grade = scoreGrades.FirstOrDefault(x => x.StudentId == id && x.Term == term && x.Subject == subject)?.Grade
                        };

                        if (subjectScores.Any(x => x.ExamType == ExamType.Homework))
                        {
                            subjectReport.HomeworkScore = (int) Math.Round((double)subjectScores.First(x => x.ExamType == ExamType.Homework).Mark);
                        }

                        if (subjectScores.Any(x => x.ExamType == ExamType.ClassActivities))
                        {
                            subjectReport.ClassActivitiesScore = (int) Math.Round((double)subjectScores.First(x => x.ExamType == ExamType.ClassActivities).Mark);
                        }
                        
                        if (subjectScores.Any(x => x.ExamType == ExamType.CA))
                        {
                            subjectReport.CAScore = (int) Math.Round((double)subjectScores.First(x => x.ExamType == ExamType.CA).Mark);
                        }

                        if (subjectScores.Any(x => x.ExamType == ExamType.FirstExam))
                        {
                            subjectReport.FirstExamScore = (int) Math.Round((double)subjectScores.First(x => x.ExamType == ExamType.FirstExam).Mark);
                        }

                        if (subjectScores.Any(x => x.ExamType == ExamType.SecondExam))
                        {
                            subjectReport.SecondExamScore = (int) Math.Round((double)subjectScores.First(x => x.ExamType == ExamType.SecondExam).Mark);
                        }


                        //var subjectReport = new SubjectReport
                        //{
                        //    Subject = subject,
                        //    HomeworkScore = (int?) subjectScores.FirstOrDefault(x => x.ExamType == ExamType.Homework)?.Mark,
                        //    ClassActivitiesScore = (int?) subjectScores.FirstOrDefault(x => x.ExamType == ExamType.ClassActivities)?.Mark,
                        //    CAScore = (int?) subjectScores.FirstOrDefault(x => x.ExamType == ExamType.CA)?.Mark,
                        //    FirstExamScore = (int?) subjectScores.FirstOrDefault(x => x.ExamType == ExamType.FirstExam)?.Mark,
                        //    SecondExamScore = (int?) subjectScores.FirstOrDefault(x => x.ExamType == ExamType.SecondExam)?.Mark,
                        //    WeightedScore = scoreGrades.FirstOrDefault(x => x.StudentId == id && x.Term == term && x.Subject == subject)?.WeightedScore,
                        //    Grade = scoreGrades.FirstOrDefault(x => x.StudentId == id && x.Term == term && x.Subject == subject)?.Grade
                        //};

                        termReport.SubjectReports.Add(subjectReport);
                    }

                    studentReport.TermReports.Add(termReport);
                }

                classReport.StudentReports.Add(studentReport);
            }

            return classReport;
        }

        private (int weightedScore, char grade) GetWeightedScoreAndGrade(IEnumerable<StudentScore> studentScores, Rule rule)
        {
            float tempScore = 0;
            char grade;

            foreach (var studentScore in studentScores)
            {
                // The following applies to a situation whereby all scores are entered in 100%
                // The associated rule is then used to calculate the weight of each score.

                // Use pattern matching
                _ = studentScore.ExamType switch
                {
                    ExamType.CA => tempScore += (studentScore.Mark * rule.CAPercentage ?? 0) / 100.0f,
                    ExamType.ClassActivities => tempScore += (studentScore.Mark * rule.ClassActivitiesPercentage ?? 0) / 100.0f,
                    ExamType.Homework => tempScore += (studentScore.Mark * rule.HomeworkPercentage ?? 0) / 100.0f,
                    ExamType.FirstExam => tempScore += (studentScore.Mark * rule.FirstExamPercentage) / 100.0f,
                    ExamType.SecondExam => tempScore += (studentScore.Mark * rule.SecondExamPercentage ?? 0) / 100.0f,
                    _ => tempScore += 0
                };
            }

            int weightedScore = (int)Math.Round(tempScore);

            //// Use pattern matching
            //grade = weightedScore switch
            //{
            //    (weightedScore >= rule.A_DistinctionCutoff) => 'A',
            //    _ => 'F'
            //};

            if (weightedScore >= rule.A_DistinctionCutoff)
            {
                grade = 'A';
            }
            else if (weightedScore >= rule.B_VeryGoodCutoff)
            {
                grade = 'B';
            }
            else if (weightedScore >= rule.C_CreditCutoff)
            {
                grade = 'C';
            }
            else if (weightedScore >= rule.P_PassCutoff)
            {
                grade = 'P';
            }
            else
            {
                grade = 'F';
            }

            return (weightedScore, grade);
        }
    }

    /// <summary>
    /// A look up table to store weighted score (computed from CA, First Exam, etc,
    /// based on the specified rule) and grade.
    /// </summary>
    class ScoreGrade 
    {
        public Guid StudentId { get; set; }
        public string Term { get; set; }
        public string Subject { get; set; }
        public int WeightedScore { get; set; }
        public char Grade { get; set; }
    }
}

