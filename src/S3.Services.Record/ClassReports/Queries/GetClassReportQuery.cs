using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;

namespace S3.Services.Record.ClassReports.Queries
{
    //public class GetClassReportQuery : GetQuery<StudentScore>, IQuery<ClassReport>
    public class GetClassReportQuery : IQuery<ClassReport>
    {
        public Guid? SchoolId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? ClassId { get; set; }
        public int Session { get; set; }

        [JsonConstructor]
        public GetClassReportQuery(Guid? schoolId, Guid? studentId, Guid? classId, int session)
            => (SchoolId, StudentId, ClassId, Session) = (schoolId, studentId, classId, session);

        //public GetClassReportQuery(Guid id, string[]? includeArray) : base(id, includeArray) { }
    }
}