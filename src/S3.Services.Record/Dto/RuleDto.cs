using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Dto
{
    public class RuleDto : BaseDto
    {
        public Guid SchoolId { get; set; }
        public string Name { get; set; }
        public float CAPercentage { get; set; }
        public float FirstExamPercentage { get; set; }
        public float SecondExamPercentage { get; set; }
        public float HomeworkPercentage { get; set; }
        public float ClassParticipationPercentage { get; set; }
        public int A_DistinctionPoint { get; set; } // e.g. 85 and above
        public int B_VeryGoodPoint { get; set; } // e.g. 70 - 84
        public int C_CreditPoint { get; set; } // e.g. 55 - 69
        public int P_PassPoint { get; set; } // e.g. 50 - 54
        public int F_FailPoint { get; set; } // e.g. 0 - 49
        public bool IsDefault { get; set; }
    }
}
