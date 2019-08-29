
using System;
using System.ComponentModel.DataAnnotations;

namespace S3.Services.Record.StudentScores.Commands
{
    public class Score 
    {
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public float StudentScore { get; set; }
    }
}