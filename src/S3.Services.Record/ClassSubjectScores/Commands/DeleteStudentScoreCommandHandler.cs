using System.Threading.Tasks;
using S3.Common.Handlers;
using S3.Common.RabbitMq;
using S3.Common.Types;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using S3.Common.Mongo;
using S3.Common;
using System.Linq;
using S3.Services.Record.Utility;

namespace S3.Services.Record.StudentScores.Commands
{
    public class DeleteStudentScoreCommandHandler : ICommandHandler<DeleteStudentScoreCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteStudentScoreCommandHandler> _logger;
        private readonly RecordDbContext _db;

        public DeleteStudentScoreCommandHandler(IBusPublisher busPublisher, ILogger<DeleteStudentScoreCommandHandler> logger, RecordDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteStudentScoreCommand command, ICorrelationContext context)
        {
            var studentScore = await _db.StudentScores.FirstOrDefaultAsync(x => x.Id == command.Id);
          
            if (studentScore is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Student Score with id: '{command.Id}' was not found.");

            _db.StudentScores.Remove(studentScore);

            await _db.SaveChangesAsync();
        }
    }
}