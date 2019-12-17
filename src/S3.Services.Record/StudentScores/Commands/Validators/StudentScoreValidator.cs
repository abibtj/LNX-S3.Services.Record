
using FluentValidation;
using S3.Services.Record.Domain;

namespace S3.Services.Record.ClassSubjectScores.Commands.Validators
{
    public class StudentScoreValidator : AbstractValidator<StudentScore>
    {
        public StudentScoreValidator()
        {
            RuleFor(x => x.StudentId)
               .NotEmpty().WithMessage("Student's Id is required.");

            RuleFor(x => x.StudentName)
                .NotEmpty().WithMessage("Student's name is required.")
                .MinimumLength(2).WithMessage("Name is too short.")
                .MaximumLength(100).WithMessage("Name is too long. Maximum of 100 characters is allowed.");

            RuleFor(x => x.SchoolId)
              .NotEmpty().WithMessage("School's Id is required.");

            RuleFor(x => x.ClassId)
              .NotEmpty().WithMessage("Class' Id is required.");

            RuleFor(x => x.ClassName)
                .MaximumLength(20).WithMessage("Class name is too long, maximum of 20 character is allowed.");

            RuleFor(x => x.Subject)
                .MaximumLength(50).WithMessage("Class name is too long, maximum of 50 character is allowed.");

            RuleFor(x => x.ExamType)
                .MaximumLength(20).WithMessage("Class name is too long, maximum of 20 character is allowed.");

            RuleFor(x => x.Term)
                .NotEmpty().WithMessage("Term is required.");

            RuleFor(x => x.Session)
                .NotEmpty().WithMessage("Session is required.");

            RuleFor(x => x.Mark)
                .InclusiveBetween(0, 100).WithMessage("Student's score must be between 0 and 100.");

            RuleFor(x => x.RuleId)
                 .NotEmpty().WithMessage("Rule's Id is required.");
        }
    }
}
