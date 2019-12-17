using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Domain
{
    /// <summary>
    /// / Not to be stored in the database. For reports generation only
    /// </summary>
    public class ClassReport
    {
        //public string ClassName { get; set; }
        public ICollection<StudentReport> StudentReports { get; set; }
        public ScoresStat ScoresStat { get; set; }
    }

   
    public class StudentReport
    {
        public string StudentName { get; set; }
        public ICollection<TermReport> TermReports { get; set; }
    }

    public class TermReport
    {
        public string Term { get; set; }
        public ICollection<SubjectReport> SubjectReports { get; set; }
    }

    public class SubjectReport
    {
        public string Subject { get; set; }
        public int? HomeworkScore { get; set; }
        public int? ClassActivitiesScore { get; set; }
        public int? CAScore { get; set; }
        public int? FirstExamScore { get; set; }
        public int? SecondExamScore { get; set; }
        public int? WeightedScore { get; set; }
        public char? Grade { get; set; }
    }

    public class ScoresStat
    {
        public ICollection<TermStat> TermStats { get; set; }
    }
    public class TermStat
    {
        public string Term { get; set; }
        public ICollection<Stat> Stats { get; set; }
    }
    public class Stat
    {
        public string Subject { get; set; }
        public int ClassMinScore { get; set; }
        public int ClassMaxScore { get; set; }
        public int ClassAverageScore { get; set; }
    }
}
