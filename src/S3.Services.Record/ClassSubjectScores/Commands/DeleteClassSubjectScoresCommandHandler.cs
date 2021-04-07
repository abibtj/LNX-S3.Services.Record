//using System.Threading.Tasks;
//using S3.Common.Handlers;
//using S3.Common.RabbitMq;
//using S3.Common.Types;
//using Microsoft.Extensions.Logging;
//using Microsoft.EntityFrameworkCore;
//using S3.Common.Mongo;
//using S3.Common;
//using System.Linq;
//using S3.Services.Record.Utility;

//namespace S3.Services.Record.ClassSubjectScores.Commands
//{
//    public class DeleteClassSubjectScoresCommandHandler : ICommandHandler<DeleteClassSubjectScoresCommand>
//    {
//        private readonly IBusPublisher _busPublisher;
//        private readonly ILogger<DeleteClassSubjectScoresCommandHandler> _logger;
//        private readonly RecordDbContext _db;

//        public DeleteClassSubjectScoresCommandHandler(IBusPublisher busPublisher, ILogger<DeleteClassSubjectScoresCommandHandler> logger, RecordDbContext db)
//            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

//        public async Task HandleAsync(DeleteClassSubjectScoresCommand command, ICorrelationContext context)
//        {
//            var classSubjectScores = await _db.ClassSubjectScores.FirstOrDefaultAsync(x => x.Id == command.Id);
           
//            if (classSubjectScores is null)
//                throw new S3Exception(ExceptionCodes.NotFound,
//                    $"Student Score with id: '{command.Id}' was not found.");

//            _db.ClassSubjectScores.Remove(classSubjectScores);

//            await _db.SaveChangesAsync();
//        }
//    }
//}