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
            await _db.StudentScores.AddRangeAsync(command.StudentScores);

            await _db.SaveChangesAsync();
        }

    }
}