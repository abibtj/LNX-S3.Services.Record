using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Dto
{
    public class StudentScoreDto : BaseDto
    {
        public Guid StudentId { get; set; }
        public Guid SubjectId { get; set; }
        public Guid ClassId { get; set; }
        public string Type { get; set; } // CA, First exam, Second exam, Homework, Class participation
        public int Term { get; set; }
        public int Session { get; set; }
        public float Score { get; set; }
    }
}
