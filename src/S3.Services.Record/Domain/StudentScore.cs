using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Domain
{
    public class StudentScore : BaseEntity
    {
        public Guid StudentId { get; set; }
        public string Subject { get; set; }
        public string Class { get; set; }
        public string Type { get; set; } // CA, First exam, Second exam, Homework, Class participation
        public int Term { get; set; }
        public int Session { get; set; }
        public float Score { get; set; }
    }
}
