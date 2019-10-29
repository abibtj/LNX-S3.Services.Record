using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Record.Dto;
using System;
using System.Collections.Generic;

namespace S3.Services.Record.ClassSubjectScores.Queries
{
    public class BrowseClassSubjectScoresQuery : BrowseQuery<Domain.ClassSubjectScores>, IQuery<IEnumerable<ClassSubjectScoresDto>>
    {
        public Guid? SchoolId { get; set; }
        public Guid? ClassId { get; set; }
        public string? Subject { get; set; }
        public string? ExamType { get; set; } // CA, First exam, Second exam, Homework, Class participation
        public int? Term { get; set; }
        public int? Session { get; set; }

        //[JsonConstructor]
        public BrowseClassSubjectScoresQuery(string[]? includeArray, Guid? schoolId, Guid? classId, string? subject, string? examType, 
            int? term, int? session, int page, int results, string orderBy, string sortOrder)
          : base(includeArray, page, results, orderBy, sortOrder)

            => (SchoolId, ClassId, Subject, ExamType, Term, Session)
            = (schoolId, classId, subject, examType, term, session);
    }
}