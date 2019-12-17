using S3.Common.Types;
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.Domain
{
    public class Rule : BaseEntity
    {
        public Guid SchoolId { get; set; }
        public string Name { get; set; }
        public float? CAPercentage { get; set; }
        public float FirstExamPercentage { get; set; } // Only first exam is compulsory
        public float? SecondExamPercentage { get; set; }
        public float? HomeworkPercentage { get; set; }
        public float? ClassActivitiesPercentage { get; set; }
        public float A_DistinctionCutoff { get; set; } // e.g. 85 and above
        public float B_VeryGoodCutoff { get; set; } // e.g. 70 - 84
        public float C_CreditCutoff { get; set; } // e.g. 55 - 69
        public float P_PassCutoff { get; set; } // e.g. 50 - 54
        public float F_FailCutoff { get; set; } // e.g. 0 - 49
        public bool IsDefault { get; set; }
    }
}
