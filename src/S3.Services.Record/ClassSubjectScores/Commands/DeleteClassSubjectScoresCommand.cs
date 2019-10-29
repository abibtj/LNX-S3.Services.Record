using System;
using S3.Common.Messages;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace S3.Services.Record.ClassSubjectScores.Commands
{
    public class DeleteClassSubjectScoresCommand : ICommand
    {
        [Required]
        public Guid Id { get; }

        [JsonConstructor]
        public DeleteClassSubjectScoresCommand(Guid id) => Id = id;
    }
}