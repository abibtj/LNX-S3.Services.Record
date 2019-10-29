
//using FluentValidation;
//using S3.Services.Record.StudentScores.Commands;
//using S3.Services.Record.Domain;

//namespace S3.Services.Record.Utility
//{
//    public class UpdateStudentScoreCommandValidator : AbstractValidator<UpdateStudentScoreCommand>
//    {
//        public UpdateStudentScoreCommandValidator()
//        {
//            RuleFor(x => x.Id)
//                .NotEmpty().WithMessage("Id is required.");

//            RuleFor(x => x.SubjectId)
//                .NotEmpty().WithMessage("Subject's Id is required.");

//            RuleFor(x => x.ClassId)
//                .NotEmpty().WithMessage("Class's Id is required.");

//            RuleFor(x => x.Term)
//                .NotEmpty().WithMessage("Term is required.");

//            RuleFor(x => x.Session)
//                .NotEmpty().WithMessage("Session is required.");

//            RuleFor(x => x.Score).SetValidator(new ScoreValidator());
//        }
//    }
//}
