//using System.Threading.Tasks;
//using S3.Common.Handlers;
//using S3.Common.RabbitMq;
//using S3.Common.Types;
//using S3.Services.Record.Domain;
//using Microsoft.Extensions.Logging;
//using S3.Common;
//using System;
//using Microsoft.EntityFrameworkCore;
//using S3.Services.Record.Utility;
//using System.Linq;
//using System.Collections.Generic;
//using S3.Services.Record.ClassSubjectScores.Commands;

//namespace S3.Services.Record.ClassSubjectScores.Commands
//{
//    public class CreateClassSubjectScoresCommandHandler : ICommandHandler<CreateClassSubjectScoresCommand>
//    {
//        private readonly IBusPublisher _busPublisher;
//        private readonly ILogger<CreateClassSubjectScoresCommandHandler> _logger;
//        private readonly RecordDbContext _db;

//        public CreateClassSubjectScoresCommandHandler(IBusPublisher busPublisher, ILogger<CreateClassSubjectScoresCommandHandler> logger, RecordDbContext db)
//            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


//        public async Task HandleAsync(CreateClassSubjectScoresCommand command, ICorrelationContext context)
//        {
//            // Create a new ClassSubjectScores
//            var classSubjectScores = new Domain.ClassSubjectScores
//            {
//                ClassId = command.ClassId,
//                ClassName = command.ClassName,
//                ExamType = command.ExamType,
//                Scores = command.Scores,
//                Session = command.Session,
//                Subject = command.Subject,
//                Term = command.Term
//            };

//            // Store individual student scores
//            var studentScores = new List<StudentScore>();
//            foreach (var score in command.Scores)
//            {
//                studentScores.Add(new StudentScore
//                {
//                    StudentId = score.StudentId,
//                    StudentName = score.StudentName,
//                    Mark = score.Mark
//                });
//            }

//            //public Guid StudentId { get; set; }
//            //public string StudentName { get; set; }
//            ////public Guid SchoolId { get; set; }
//            //public Guid ClassId { get; set; }
//            //public string ClassName { get; set; }
//            //public string Subject { get; set; }
//            //public string ExamType { get; set; } // CA, First exam, Second exam, Homework, Class Activities
//            //public int Term { get; set; }
//            //public int Session { get; set; }
//            //public float Mark { get; set; }
//            await _db.StudentScores.AddRangeAsync(studentScores);
//            await _db.ClassSubjectScores.AddAsync(classSubjectScores);

//            //TODO: Publish an event
//        }
//    }
//}