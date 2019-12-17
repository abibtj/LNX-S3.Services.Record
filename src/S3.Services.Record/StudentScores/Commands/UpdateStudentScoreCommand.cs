using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using S3.Services.Record.Domain;
using System.Collections.Generic;
using S3.Common.Types;
using S3.Common;

namespace S3.Services.Record.StudentScores.Commands
{
    public class UpdateStudentScoreCommand : ICommand
    {
        [Required]
        public ICollection<StudentScore> StudentScores { get; set; }

        [JsonConstructor]
        public UpdateStudentScoreCommand(ICollection<StudentScore> studentScores)
        {
            StudentScores = studentScores;
        }
    }
}