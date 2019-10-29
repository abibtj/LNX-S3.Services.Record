
using FluentValidation;
using S3.Services.Record.ClassSubjectScores.Commands;
using S3.Services.Record.ClassSubjectScores.Commands.Validators;
using S3.Services.Record.Domain;

namespace S3.Services.Record.ClassSubjectScores.Commands.Validators
{
    public class UpdateClassSubjectScoresCommandValidator : AbstractValidator<UpdateClassSubjectScoresCommand>
    {
        public UpdateClassSubjectScoresCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.");

            RuleFor(x => x.ClassName)
                .MaximumLength(20).WithMessage("Class name is too long, maximum of 20 character is allowed.");

            RuleFor(x => x.Subject)
                .MaximumLength(50).WithMessage("Class name is too long, maximum of 50 character is allowed.");
           
            RuleFor(x => x.ExamType)
                .MaximumLength(20).WithMessage("Class name is too long, maximum of 20 character is allowed.");

            RuleFor(x => x.ClassId)
                .NotEmpty().WithMessage("Class's Id is required.");

            RuleFor(x => x.Term)
                .NotEmpty().WithMessage("Term is required.");

            RuleFor(x => x.Session)
                .NotEmpty().WithMessage("Session is required.");

            RuleFor(x => x.Scores)
                .NotNull().WithMessage("Student scores are required.");

            RuleForEach(x => x.Scores).SetValidator(new ScoreValidator());
        }
    }
}
