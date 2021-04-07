//using System;
//using S3.Common.Messages;
//using Newtonsoft.Json;
//using System.ComponentModel.DataAnnotations;
//using S3.Services.Record.Domain;
//using System.Collections.Generic;
//using S3.Common.Types;
//using S3.Common;

//namespace S3.Services.Record.StudentScores.Commands
//{
//    public class UpdateStudentScoreCommand : ICommand
//    {
//        //[Required]
//        //public Guid SchoolId { get; set; }
//        //[Required]
//        //public Guid ClassId { get; set; }
//        //[Required]
//        //public string Subject { get; set; }
//        //[Required]
//        //public string ExamType { get; set; } // CA, First exam, Second exam, Homework, Class Activities
//        //[Required]
//        //public int Term { get; set; }
//        //[Required]
//        //public int Session { get; set; }

//        [Required]
//        public ICollection<StudentScore> StudentScores { get; set; }

//        [JsonConstructor]
//        public UpdateStudentScoreCommand(ICollection<StudentScore> studentScores)
//        {
//            StudentScores = studentScores;
//        }
//    }
//}