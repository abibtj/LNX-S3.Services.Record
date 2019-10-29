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

//namespace S3.Services.Record.StudentScores.Commands
//{
//    public class UpdateStudentScoreCommandHandler : ICommandHandler<UpdateStudentScoreCommand>
//    {
//        private readonly IBusPublisher _busPublisher;
//        private readonly ILogger<UpdateStudentScoreCommandHandler> _logger;
//        private readonly RecordDbContext _db;

//        public UpdateStudentScoreCommandHandler(IBusPublisher busPublisher, ILogger<UpdateStudentScoreCommandHandler> logger, RecordDbContext db)
//            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

//        public async Task HandleAsync(UpdateStudentScoreCommand command, ICorrelationContext context)
//        {
//            // Get existing studentScore
//            var studentScore = await _db.StudentScores.FirstOrDefaultAsync(x => x.Id == command.Id);
//            if (studentScore is null)
//                throw new S3Exception(ExceptionCodes.NotFound,
//                    $"Student Score with id: '{command.Id}' was not found.");

//            studentScore.ClassId = command.ClassId;
//            studentScore.StudentId = command.Score.StudentId;
//            studentScore.SubjectId = command.SubjectId;
//            studentScore.Type = command.Type;
//            studentScore.Term = command.Term;
//            studentScore.Session = command.Session;
//            studentScore.Score = command.Score.StudentScore;
//            studentScore.SetUpdatedDate();

//            await _db.SaveChangesAsync();
//        }
//    }
//}