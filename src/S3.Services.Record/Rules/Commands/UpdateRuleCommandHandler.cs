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

namespace S3.Services.Record.Rules.Commands
{
    public class UpdateRuleCommandHandler : ICommandHandler<UpdateRuleCommand>
    {
        private readonly IBusPublisher _busPublisher;
        private readonly ILogger<UpdateRuleCommandHandler> _logger;
        private readonly RecordDbContext _db;

        public UpdateRuleCommandHandler(IBusPublisher busPublisher, ILogger<UpdateRuleCommandHandler> logger, RecordDbContext db)
            => (_busPublisher, _logger, _db) = (busPublisher, logger, db);

        public async Task HandleAsync(UpdateRuleCommand command, ICorrelationContext context)
        {
            // Get existing rule
            var rule = await _db.Rules.FirstOrDefaultAsync(x => x.Id == command.Id);
            if (rule is null)
                throw new S3Exception(ExceptionCodes.NotFound,
                    $"Rule with id: '{command.Id}' was not found.");

            // Check for existence of a rule with the same name in this school.
            if (await _db.Rules.AnyAsync(x => (x.SchoolId == command.SchoolId) &&
                x.Name.ToLowerInvariant() == Normalizer.NormalizeSpaces(command.Name).ToLowerInvariant()
                && (x.Id != command.Id)))
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

            rule.Name = Normalizer.NormalizeSpaces(command.Name);
            rule.CAPercentage = command.CAPercentage;
            rule.FirstExamPercentage = command.FirstExamPercentage;
            rule.SecondExamPercentage = command.SecondExamPercentage;
            rule.HomeworkPercentage = command.HomeworkPercentage;
            rule.ClassActivitiesPercentage = command.ClassActivitiesPercentage;
            rule.IsDefault = command.IsDefault;
            rule.SchoolId = command.SchoolId;
            rule.A_DistinctionCutoff = command.A_DistinctionCutoff;
            rule.B_VeryGoodCutoff = command.B_VeryGoodCutoff;
            rule.C_CreditCutoff = command.C_CreditCutoff;
            rule.P_PassCutoff = command.P_PassCutoff;
            rule.F_FailCutoff = command.F_FailCutoff;

            rule.SetUpdatedDate();

            await _db.SaveChangesAsync();
        }
    }
}