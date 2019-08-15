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

namespace S3.Services.Record.Rules.Commands
{
    public class DeleteRuleCommandHandler : ICommandHandler<DeleteRuleCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<DeleteRuleCommandHandler> _logger;
        private readonly RecordDbContext _db;

        public DeleteRuleCommandHandler(IBusPublisher busPublisher, ILogger<DeleteRuleCommandHandler> logger, RecordDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(DeleteRuleCommand command, ICorrelationContext context)
        {
            var rule = await _db.Rules.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (rule is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Rule with id: '{command.Id}' was not found.");

            _db.Rules.Remove(rule); 

            await _db.SaveChangesAsync();
        }
    }
}