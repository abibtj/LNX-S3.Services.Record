using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Services.Record.Domain;
using System.Collections.Generic;
using S3.Common.Types;
using S3.Common;

namespace S3.Services.Record.ClassSubjectScores.Commands
{
    public class CreateClassSubjectScoresCommand : ICommand
    {
        [Required]
        public Guid ClassId { get; set; }
        [Required(ErrorMessage = "Class name is required.")]
        public string ClassName { get; set; }
        [Required(ErrorMessage = "Subject is required.")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "Exam type is required.")]
        public string ExamType { get; set; } // CA, First exam, Second exam, Homework, Class participation
        [Required]
        public int Term { get; set; }
        [Required]
        public int Session { get; set; }
        [Required]
        public ICollection<Score> Scores { get; set; }

        [JsonConstructor]
        public CreateClassSubjectScoresCommand(Guid classId, string className, string subject, string examType,
            int term, int session, ICollection<Score> scores)
        {
            (ClassId, ClassName, Subject, ExamType, Term, Session, Scores)
                = (classId, className, subject, examType, term, session, scores);
        }
    }
}