using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using S3.Common.Mongo;
using S3.Common;
using System.Text.RegularExpressions;
using System;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using S3.Services.Record.Utility;
using S3.Services.Record.Domain;

namespace S3.Services.Record.StudentScores.Commands
{
    public class UpdateStudentScoreCommandHandler : ICommandHandler<UpdateStudentScoreCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateStudentScoreCommandHandler> _logger;
        private readonly RecordDbContext _db;

        public UpdateStudentScoreCommandHandler(IBusPublisher busPublisher, ILogger<UpdateStudentScoreCommandHandler> logger, RecordDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(UpdateStudentScoreCommand command, ICorrelationContext context)
        {
            // Get existing studentScores
            var sampleScore = command.StudentScores.First();
            var existingStudentScores = _db.StudentScores.Where(x => x.SchoolId == sampleScore.SchoolId && 
            x.ClassId == sampleScore.ClassId && x.ExamType == sampleScore.ExamType && x.Session == sampleScore.Session
            && x.Term == sampleScore.Term );

            if (!existingStudentScores.Any()) // Nothing exists, add the student scores afresh
            {
                await _db.StudentScores.AddRangeAsync(command.StudentScores);
            }
            else 
            {
                // Create / Update existing scores
                var existingScores_StudentIds = new HashSet<Guid>(existingStudentScores.Select(x => x.StudentId)).ToList();

                var newScores = new List<StudentScore> { };

                // Some or all of the scores have been previously saved
                // Update the changes and/or create the newly added ones.
                foreach (var score in command.StudentScores)
                {
                    if (!existingScores_StudentIds.Contains(score.StudentId)) // This student has no score for this subject yet, add a new score
                        newScores.Add(score);
                    else // Update the score (if neccessary)
                    {
                        var studentScore = existingStudentScores.FirstOrDefault(x => x.StudentId == score.StudentId);
                        if (studentScore.Mark != score.Mark)
                        {
                            studentScore.Mark = score.Mark;
                            studentScore.SetUpdatedDate();
                        }
                    }
                }

                // Check the newly sent scores and see if any existing scores have been removed.
                var newScores_StudentIds = new HashSet<Guid>(command.StudentScores.Select(x => x.StudentId)).ToList();

                foreach (var studentId in existingScores_StudentIds)
                {
                    if (!newScores_StudentIds.Contains(studentId)) // This student had a score before, but has now been removed.
                    {
                        var studentScore = existingStudentScores.FirstOrDefault(x => x.StudentId == studentId);
                        _db.StudentScores.Remove(studentScore);
                    }
                }

                if (newScores.Count > 0)
                    await _db.StudentScores.AddRangeAsync(newScores);
            }

            await _db.SaveChangesAsync();
        }
    }
}