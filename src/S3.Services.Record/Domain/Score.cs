using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Domain
{
    public class Score : BaseEntity
    {
        public Guid SchoolId { get; set; }
        public string Name { get; set; }
        public float CAPercentage { get; set; }
        public float FirstExamPercentage { get; set; }
        public float SecondExamPercentage { get; set; }
        public float HomeworkPercentage { get; set; }
        public float ClassParticipationPercentage { get; set; }
        public bool IsDefault { get; set; }
    }
}
