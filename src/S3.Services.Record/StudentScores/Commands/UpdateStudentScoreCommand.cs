using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Services.Record.Domain;
using System.Collections.Generic;
using S3.Common.Types;
using S3.Common;

namespace S3.Services.Record.StudentScores.Commands
{
    public class UpdateStudentScoreCommand : ICommand
    {
        [Required]
        public Guid Id { get; set; }
        [Required]
        public Guid SubjectId { get; set; }
        [Required]
        public Guid ClassId { get; set; }
        [Required(ErrorMessage ="Type is required.")]
        public string Type { get; set; } // CA, First exam, Second exam, Homework, Class participation
        [Required]
        public int Term { get; set; }
        [Required]
        public int Session { get; set; }
        [Required(ErrorMessage = "Score is required.")]
        public Score Score { get; set; } // StudentIds + Scores

        [JsonConstructor]
        public UpdateStudentScoreCommand(Guid id, Guid subjectId, Guid classId, string type, int term, int session, Score score)
        {
            (Id, SubjectId, ClassId, Type, Term, Session, Score)
                = (id, subjectId, classId, type, term, session, score);
        }
    }
}