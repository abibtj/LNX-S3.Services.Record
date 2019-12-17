using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Common.Types;

namespace S3.Services.Record.Rules.Commands
{
    public class UpdateRuleCommand : ICommand
    {
        [Required]
        public Guid Id { get; }
        [Required]
        public Guid SchoolId { get; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; }
        public float? CAPercentage { get; }
        public float FirstExamPercentage { get; }
        public float? SecondExamPercentage { get; }
        public float? HomeworkPercentage { get; }
        public float? ClassActivitiesPercentage { get; }
        public float TotalPercentage { get; }
        public float A_DistinctionCutoff { get; } // e.g. 85 and above
        public float B_VeryGoodCutoff { get; } // e.g. 70 - 84
        public float C_CreditCutoff { get; } // e.g. 55 - 69
        public float P_PassCutoff { get; } // e.g. 50 - 54
        public float F_FailCutoff { get; } // e.g. 0 - 49
        public bool IsDefault { get; }

        [JsonConstructor]
        public UpdateRuleCommand(Guid id, Guid schoolId, string name, float? cAPercentage, float firstExamPercentage,
            float? secondExamPercentage, float? homeworkPercentage, float? classActivitiesPercentage,
            float a_DistinctionCutoff, float b_VeryGoodCutoff, float c_CreditCutoff, float p_PassCutoff,
            float f_FailCutoff, bool isDefault)
        {
            (Id, SchoolId, Name, CAPercentage, FirstExamPercentage, SecondExamPercentage, HomeworkPercentage, ClassActivitiesPercentage,
                A_DistinctionCutoff, B_VeryGoodCutoff, C_CreditCutoff, P_PassCutoff, F_FailCutoff, IsDefault)

                = (id, schoolId, name, cAPercentage, firstExamPercentage, secondExamPercentage, homeworkPercentage, classActivitiesPercentage,
                a_DistinctionCutoff, b_VeryGoodCutoff, c_CreditCutoff, p_PassCutoff, f_FailCutoff, isDefault);

            //TotalPercentage = cAPercentage + firstExamPercentage + secondExamPercentage ?? 0f + homeworkPercentage ?? 0f + classActivitiesPercentage ?? 0f; // Not working
            TotalPercentage = firstExamPercentage;
            TotalPercentage += cAPercentage ?? 0f;
            TotalPercentage += secondExamPercentage ?? 0f;
            TotalPercentage += homeworkPercentage ?? 0f;
            TotalPercentage += classActivitiesPercentage ?? 0f;
        }
    }
}