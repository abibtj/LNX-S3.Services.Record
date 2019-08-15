
using FluentValidation;
using S3.Services.Record.Rules.Commands;
using S3.Services.Record.Domain;

namespace S3.Services.Record.Utility
{
    public class CreateRuleCommandValidator : AbstractValidator<CreateRuleCommand>
    {
        public CreateRuleCommandValidator()
        {
            RuleFor(x => x.SchoolId)
                .NotEmpty().WithMessage("School's Id is required.");

            RuleFor(x => x.TotalPercentage)
                .Equal(100).WithMessage("The total percentage must be 100.");
        }
    }
}
