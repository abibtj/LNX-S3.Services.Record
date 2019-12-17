
using FluentValidation;
using S3.Services.Record.Rules.Commands;
using S3.Services.Record.Domain;

namespace S3.Services.Record.Utility
{
    public class UpdateRuleCommandValidator : AbstractValidator<UpdateRuleCommand>
    {
        public UpdateRuleCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.SchoolId)
                 .NotEmpty().WithMessage("School's Id is required.");

            RuleFor(x => x.Name)
                   .NotEmpty().WithMessage("Name is required.")
                   .MinimumLength(2).When(x => !string.IsNullOrEmpty(x.Name)).WithMessage("Name is too short.")
                   .MaximumLength(20).WithMessage("Name is too long. Maximum of 20 characters is allowed.");

            RuleFor(x => x.CAPercentage)
                    //.NotEmpty().WithMessage("CA Percentage is required.")
                    .InclusiveBetween(0, 100).WithMessage("CA Percentage value must be between 0 and 100.");

            RuleFor(x => x.FirstExamPercentage)
                    .NotEmpty().WithMessage("First Exam Percentage is required.")
                   .InclusiveBetween(0, 100).WithMessage("First Exam Percentage value must be between 0 and 100.");

            RuleFor(x => x.SecondExamPercentage)
                   .InclusiveBetween(0, 100).WithMessage("Second Exam Percentage value must be between 0 and 100.");

            RuleFor(x => x.HomeworkPercentage)
                   .InclusiveBetween(0, 100).WithMessage("Homework Percentage value must be between 0 and 100.");

            RuleFor(x => x.ClassActivitiesPercentage)
                   .InclusiveBetween(0, 100).WithMessage("Class Activities Percentage value must be between 0 and 100.");

            RuleFor(x => x.TotalPercentage)
               .Equal(100.0f).WithMessage("The total percentage must be 100.");

            RuleFor(x => x.A_DistinctionCutoff)
               .InclusiveBetween(0, 100).WithMessage("'A' Distinction Cutoff mark must be between 0 and 100.");

            RuleFor(x => x.B_VeryGoodCutoff)
                   .InclusiveBetween(0, 100).WithMessage("'B' Very Good Cutoff mark must be between 0 and 100.");

            RuleFor(x => x.C_CreditCutoff)
                   .InclusiveBetween(0, 100).WithMessage("'C' Credit Cutoff mark must be between 0 and 100.");

            RuleFor(x => x.P_PassCutoff)
                   .InclusiveBetween(0, 100).WithMessage("'P' Pass Cutoff mark must be between 0 and 100.");

            RuleFor(x => x.F_FailCutoff)
                   .InclusiveBetween(0, 100).WithMessage("'F' Fail Cutoff mark value must be between 0 and 100.");
        }
    }
}
