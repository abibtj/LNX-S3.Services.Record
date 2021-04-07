
//using FluentValidation;
//using S3.Services.Record.Domain;

//namespace S3.Services.Record.ClassSubjectScores.Commands.Validators
//{
//    public class ScoreValidator : AbstractValidator<Score>
//    {
//        public ScoreValidator()
//        {
//            RuleFor(x => x.StudentId)
//                .NotEmpty().WithMessage("Student's Id is required.");

//            RuleFor(x => x.StudentName)
//                .NotEmpty().WithMessage("Student's name is required.")
//                .MinimumLength(2).WithMessage("Name is too short.")
//                .MaximumLength(100).WithMessage("Name is too long. Maximum of 100 characters is allowed.");

//            RuleFor(x => x.Mark)
//                .InclusiveBetween(0, 100).WithMessage("Student's score must be between 0 and 100.");
//        }
//    }
//}
