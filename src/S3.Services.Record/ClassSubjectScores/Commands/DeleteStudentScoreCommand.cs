//using System;
//using S3.Common.Messages;
//using Newtonsoft.Json;
//using System.ComponentModel.DataAnnotations;
//using MongoDB.Bson.Serialization.Attributes;
//using MongoDB.Bson;

//namespace S3.Services.Record.StudentScores.Commands
//{
//    public class DeleteStudentScoreCommand : ICommand
//    {
//        [Required]
//        public Guid Id { get; }

//        [JsonConstructor]
//        public DeleteStudentScoreCommand(Guid id) => Id = id;
//    }
//}