
using FluentValidation;
using S3.Services.Record.StudentScores.Commands;
using S3.Services.Record.Domain;
using S3.Services.Record.ClassSubjectScores.Commands.Validators;

namespace S3.Services.Record.ClassSubjectScores.Commands.Validators
{
    public class CreateStudentScoreCommandValidator : AbstractValidator<CreateStudentScoreCommand>
    {
        public CreateStudentScoreCommandValidator()
        {
            RuleFor(x => x.StudentScores)
                .NotNull().WithMessage("Student scores are required.");

            RuleForEach(x => x.StudentScores).SetValidator(new StudentScoreValidator());
        }
    }
}
