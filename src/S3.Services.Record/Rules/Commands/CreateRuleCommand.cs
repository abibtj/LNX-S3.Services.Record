using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Services.Record.Domain;
using System.Collections.Generic;
using S3.Common.Types;
using S3.Common;

namespace S3.Services.Record.Rules.Commands
{
    public class CreateRuleCommand : ICommand
    {
        [Required]
        public Guid SchoolId { get; }
        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; }
        public float CAPercentage { get; }
        public float FirstExamPercentage { get; }
        public float SecondExamPercentage { get; }
        public float HomeworkPercentage { get; }
        public float ClassParticipationPercentage { get; }
        public float TotalPercentage { get; }
        public bool IsDefault { get; }

        [JsonConstructor]
        public CreateRuleCommand(Guid schoolId, string name, float cAPercentage, float firstExamPercentage,
            float secondExamPercentage, float homeworkPercentage, float classParticipationPercentage, bool isDefault)
        {
            (SchoolId, Name, CAPercentage, FirstExamPercentage, SecondExamPercentage, HomeworkPercentage, ClassParticipationPercentage, IsDefault)
                = (schoolId, name, cAPercentage, firstExamPercentage, secondExamPercentage, homeworkPercentage, classParticipationPercentage, isDefault);

            TotalPercentage = cAPercentage + firstExamPercentage + secondExamPercentage + homeworkPercentage + classParticipationPercentage;
        }
    }
}