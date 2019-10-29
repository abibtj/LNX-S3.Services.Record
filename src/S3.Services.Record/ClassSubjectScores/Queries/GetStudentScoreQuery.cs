//using System;
//using System.ComponentModel.DataAnnotations;
//using Newtonsoft.Json;
//using S3.Common.Types;
//using S3.Services.Record.Dto;

//namespace S3.Services.Record.StudentScores.Queries
//{
//    public class GetStudentScoreQuery : IQuery<StudentScoreDto>
//    {
//        public Guid Id { get; }

//        [JsonConstructor]
//        public GetStudentScoreQuery(Guid id) => Id = id;
//    }
//}