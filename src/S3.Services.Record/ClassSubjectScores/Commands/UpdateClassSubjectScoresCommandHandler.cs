//using System.Threading.Tasks;
//using S3.Common.Handlers;
//using S3.Common.RabbitMq;
//using S3.Common.Types;
//using Microsoft.Extensions.Logging;
//using S3.Common.Mongo;
//using S3.Common;
//using System.Text.RegularExpressions;
//using System;
//using Microsoft.EntityFrameworkCore;
//using System.Collections.Generic;
//using System.Linq;
//using S3.Services.Record.Utility;
//using S3.Services.Record.ClassSubjectScores.Commands;
//using S3.Services.Record.Domain;

//namespace S3.Services.Record.ClassSubjectScores.Commands
//{
//    public class UpdateClassSubjectScoresCommandHandler : ICommandHandler<UpdateClassSubjectScoresCommand>
//    {
//        private readonly IBusPublisher _busPublisher;
//        private readonly ILogger<UpdateClassSubjectScoresCommandHandler> _logger;
//        private readonly RecordDbContext _db;

//        public UpdateClassSubjectScoresCommandHandler(IBusPublisher busPublisher, ILogger<UpdateClassSubjectScoresCommandHandler> logger, RecordDbContext db)
//            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

//        public async Task HandleAsync(UpdateClassSubjectScoresCommand command, ICorrelationContext context)
//        {
//            // Get existing classSubjectScores
//            var classSubjectScores = await _db.ClassSubjectScores.Include(x => x.Scores).FirstOrDefaultAsync(x => x.Id == command.Id);
            
//            if (classSubjectScores is null)
//                throw new S3Exception(ExceptionCodes.NotFound,
//                    $"Class Subject Score with id: '{command.Id}' was not found.");

//            classSubjectScores.ClassId = command.ClassId;
//            classSubjectScores.ClassName = command.ClassName;
//            classSubjectScores.ExamType = command.ExamType;
//            classSubjectScores.Session = command.Session;
//            classSubjectScores.Subject = command.Subject;
//            classSubjectScores.Term = command.Term;

//            // Create / Update existing scores
//            var existingScores = classSubjectScores.Scores;
//            var existingScores_StudentIds = new HashSet<Guid>(existingScores.Select(x => x.StudentId)).ToList();

//            var newScores = new List<Score> { };

//            if (existingScores.Count() <= 0) // The previously entered scores have been removed.
//            {
//                classSubjectScores.Scores = command.Scores;
//            }
//            else
//            {
//                // Some or all of the scores have been previously saved
//                // Update the changes and/or create the newly added ones.
//                foreach (var score in command.Scores)
//                {
//                    if (!existingScores_StudentIds.Contains(score.StudentId)) // This student has no score for this subject yet, add a new score
//                        newScores.Add(score);
//                    else // Update the score (if neccessary)
//                    {
//                        var studentScore = existingScores.FirstOrDefault(x => x.StudentId == score.StudentId);
//                        if (studentScore.Mark != score.Mark)
//                            studentScore.Mark = score.Mark;
//                    }
//                }

//                // Check the newly sent scores and see if any existing scores have been removed.
//                var newScores_StudentIds = new HashSet<Guid>(command.Scores.Select(x => x.StudentId)).ToList();

//                foreach (var studentId in existingScores_StudentIds)
//                {
//                    if (!newScores_StudentIds.Contains(studentId)) // This student had a score before, but has now been removed.
//                    {
//                        var studentScore = existingScores.FirstOrDefault(x => x.StudentId == studentId);
//                        _db.Scores.Remove(studentScore);
//                    }
//                }
//            }

//            if (newScores.Count > 0)
//                await _db.Scores.AddRangeAsync(newScores);

//            classSubjectScores.SetUpdatedDate();

//            await _db.SaveChangesAsync();
//        }
//    }
//}