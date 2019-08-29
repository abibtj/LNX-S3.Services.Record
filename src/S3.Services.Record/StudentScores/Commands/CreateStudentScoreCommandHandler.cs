using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using S3.Services.Record.Domain;
using Microsoft.Extensions.Logging;
using S3.Common;
using System;
using Microsoft.EntityFrameworkCore;
using S3.Services.Record.Utility;
using System.Linq;
using System.Collections.Generic;

namespace S3.Services.Record.StudentScores.Commands
{
    public class CreateStudentScoreCommandHandler : ICommandHandler<CreateStudentScoreCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateStudentScoreCommandHandler> _logger;
        private readonly RecordDbContext _db;

        public CreateStudentScoreCommandHandler(IBusPublisher busPublisher, ILogger<CreateStudentScoreCommandHandler> logger, RecordDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateStudentScoreCommand command, ICorrelationContext context)
        {
            var existingStudentScores = _db.StudentScores.Where(x => x.ClassId == command.ClassId
            && x.SubjectId == command.SubjectId && x.Type == command.Type && x.Term == command.Term
            && x.Session == command.Session);

            var existingStudentScores_StudentIds = new HashSet<Guid>(existingStudentScores.Select(x => x.StudentId)).ToList();

            var studentScores = new List<StudentScore> { };

            if (existingStudentScores.Count() <= 0) // This is the first time of entering scores for this subject, class, term, session, 
                                                    //reate a new list of studentScores
            {
                foreach (var score in command.Scores)
                    studentScores.Add(NewStudentScore(score, command));
            }
            else
            {
                // Some or all of these student scores have been previously saved
                // Update the changes and/or create the newly added ones.
                foreach (var score in command.Scores)
                {
                    if (!existingStudentScores_StudentIds.Contains(score.StudentId)) // Add a new student score
                        studentScores.Add(NewStudentScore(score, command));
                    else // Update the score (if neccessary)
                    {
                        var studentScore = existingStudentScores.FirstOrDefault(x => x.StudentId == score.StudentId);
                        if (studentScore.Score != score.StudentScore)
                            studentScore.Score = score.StudentScore;
                    }
                }

                var studentScores_StudentIds = new HashSet<Guid>(command.Scores.Select(x => x.StudentId)).ToList();

                foreach (var studentId in existingStudentScores_StudentIds)
                {
                    if (!studentScores_StudentIds.Contains(studentId))
                    {
                        var studentScore = existingStudentScores.FirstOrDefault(x => x.StudentId == studentId);
                        _db.StudentScores.Remove(studentScore);
                    }
                }
            }

            if (studentScores.Count > 0)
                await _db.StudentScores.AddRangeAsync(studentScores);

            await _db.SaveChangesAsync();
        }

        private StudentScore NewStudentScore(Score score, CreateStudentScoreCommand command)
        {
            return new StudentScore
            {
                ClassId = command.ClassId,
                StudentId = score.StudentId,
                SubjectId = command.SubjectId,
                Type = command.Type,
                Term = command.Term,
                Session = command.Session,
                Score = score.StudentScore
            };
        }
    }
}