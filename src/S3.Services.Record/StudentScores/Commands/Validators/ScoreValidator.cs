
using FluentValidation;
using S3.Services.Record.StudentScores.Commands;
using S3.Services.Record.Domain;

namespace S3.Services.Record.Utility
{
    public class ScoreValidator : AbstractValidator<Score>
    {
        public ScoreValidator()
        {
            RuleFor(x => x.StudentId)
                .NotEmpty().WithMessage("Student's Id is required.");

            RuleFor(x => x.StudentScore)
                .InclusiveBetween(0, 100).WithMessage("Student's score must be between 0 and 100.");
        }
    }
}
