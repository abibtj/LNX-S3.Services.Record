using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Dto
{
    public class StudentScoreDto : BaseDto
    {
        public Guid StudentId { get; set; }
        public string StudentName { get; set; }
        public Guid SchoolId { get; set; }
        public Guid ClassId { get; set; }
        public string ClassName { get; set; }
        public string Subject { get; set; }
        public string ExamType { get; set; } // CA, First exam, Second exam, Homework, Class Activities
        public int Term { get; set; }
        public int Session { get; set; }
        public float Mark { get; set; }
    }
}
