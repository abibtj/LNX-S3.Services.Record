
using FluentValidation;
using S3.Services.Record.StudentScores.Commands;
using S3.Services.Record.Domain;
using S3.Services.Record.ClassSubjectScores.Commands.Validators;

namespace S3.Services.Record.Utility
{
    public class UpdateStudentScoreCommandValidator : AbstractValidator<UpdateStudentScoreCommand>
    {
        public UpdateStudentScoreCommandValidator()
        {
            //RuleFor(x => x.SchoolId)
            //    .NotEmpty().WithMessage("School's Id is required.");
           
            //RuleFor(x => x.ClassId)
            //    .NotEmpty().WithMessage("Class's Id is required.");
           
            //RuleFor(x => x.Subject)
            //    .NotEmpty().WithMessage("Subject is required.")
            //    .MaximumLength(30).WithMessage("Subject is too long, maximum of 30 characters is allowed.");
           
            //RuleFor(x => x.ExamType)
            //    .NotEmpty().WithMessage("ExamType is required.")
            //    .MaximumLength(30).WithMessage("ExamType is too long, maximum of 30 characters is allowed.");

            //RuleFor(x => x.Term)
            //    .NotEmpty().WithMessage("Term is required.")
            //    .InclusiveBetween(1,3).WithMessage("Term can only be 1, 2 or 3.");

            //RuleFor(x => x.Session)
            //    .NotEmpty().WithMessage("Session is required.")
            //    .GreaterThanOrEqualTo(2000).WithMessage("Invalid session.");

            RuleFor(x => x.StudentScores)
                 .NotNull().WithMessage("Student scores are required.");

            RuleForEach(x => x.StudentScores).SetValidator(new StudentScoreValidator());
        }
    }
}
