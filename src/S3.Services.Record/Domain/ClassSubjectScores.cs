using S3.Common.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Domain
{
    public class ClassSubjectScores : BaseEntity
    {
        public ClassSubjectScores()
        {
            Scores = new HashSet<Score>();
        }

        public Guid SchoolId { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public string Subject { get; set; }
        public string ExamType { get; set; } // CA, First exam, Second exam, Homework, Class participation
        public int Term { get; set; }
        public int Session { get; set; }
        public virtual ICollection<Score> Scores { get; set; }
    }
}
