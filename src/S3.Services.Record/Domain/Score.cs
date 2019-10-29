using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Domain
{
    public class Score : BaseEntity
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public float Mark { get; set; }
        public Guid ClassSubjectScoresId { get; set; }
        public virtual ClassSubjectScores ClassSubjectScores { get; set; }
    }
}
