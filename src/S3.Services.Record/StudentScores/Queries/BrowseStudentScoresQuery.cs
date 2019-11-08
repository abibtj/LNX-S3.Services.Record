using S3.Common.Types;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;
using System;
using System.Collections.Generic;

namespace S3.Services.Record.StudentScores.Queries
{
    public class BrowseStudentScoresQuery : BrowseQuery<StudentScore>, IQuery<IEnumerable<StudentScoreDto>>
    {
        public Guid? SchoolId { get; set; }
        public Guid? StudentId { get; set; }
        public Guid? ClassId { get; set; }
        public string? Subject { get; set; }
        public string? ExamType { get; set; } // CA, First exam, Second exam, Homework, Class Activities
        public int? Term { get; set; }
        public int? Session { get; set; }

        //[JsonConstructor]
        public BrowseStudentScoresQuery(string[]? includeArray, Guid? schoolId, Guid? studentId, Guid? classId, string? subject, string? examType,
            int? term, int? session, int page, int results, string orderBy, string sortOrder)
          : base(includeArray, page, results, orderBy, sortOrder)

            => (SchoolId, StudentId, ClassId, Subject, ExamType, Term, Session)
            = (schoolId, studentId, classId, subject, examType, term, session);
    }
}