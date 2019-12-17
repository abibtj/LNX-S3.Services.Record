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

namespace S3.Services.Record.Rules.Commands
{
    public class CreateRuleCommandHandler : ICommandHandler<CreateRuleCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<CreateRuleCommandHandler> _logger;
        private readonly RecordDbContext _db;

        public CreateRuleCommandHandler(IBusPublisher busPublisher, ILogger<CreateRuleCommandHandler> logger, RecordDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);


        public async Task HandleAsync(CreateRuleCommand command, ICorrelationContext context)
        {
            // Check for existence of a rule with the same name in this school.
            if (await _db.Rules.AnyAsync(x => (x.SchoolId == command.SchoolId) &&
                x.Name.ToLowerInvariant() == Normalizer.NormalizeSpaces(command.Name).ToLowerInvariant()))
            {
                throw new S3Exception(ExceptionCodes.NameInUse,
                    $"Rule name: '{command.Name}' is already in use.");
            }

            if (command.IsDefault)// Check if any default rule exists and make it non-default
            {
                var schoolRules = _db.Rules.Where(x => x.SchoolId == command.SchoolId);
                foreach (var _rule in schoolRules)
                {
                    if (_rule.IsDefault)
                        _rule.IsDefault = false;
                }
            }

           

            // Create a new rule
            var rule = new Rule
            {
                Name = Normalizer.NormalizeSpaces(command.Name),
                CAPercentage = command.CAPercentage,
                FirstExamPercentage = command.FirstExamPercentage,
                SecondExamPercentage = command.SecondExamPercentage,
                HomeworkPercentage = command.HomeworkPercentage,
                ClassActivitiesPercentage = command.ClassActivitiesPercentage,
                IsDefault = command.IsDefault,
                SchoolId = command.SchoolId,
                A_DistinctionCutoff = command.A_DistinctionCutoff,
                B_VeryGoodCutoff = command.B_VeryGoodCutoff,
                C_CreditCutoff = command.C_CreditCutoff,
                P_PassCutoff = command.P_PassCutoff,
                F_FailCutoff = command.F_FailCutoff
            };

            await _db.Rules.AddAsync(rule);

            await _db.SaveChangesAsync();
        }
    }
}