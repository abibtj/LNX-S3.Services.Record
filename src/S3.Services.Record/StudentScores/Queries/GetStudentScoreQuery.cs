using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;

namespace S3.Services.Record.StudentScores.Queries
{
    public class GetStudentScoreQuery : GetQuery<StudentScore>, IQuery<StudentScoreDto>
    {
        [JsonConstructor]
        public GetStudentScoreQuery(Guid id, string[]? includeArray) : base(id, includeArray) { }
    }
}