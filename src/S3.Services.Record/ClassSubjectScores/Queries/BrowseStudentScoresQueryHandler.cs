//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using S3.Common.Handlers;
//using S3.Common.Mongo;
//using S3.Common.Types;
//using S3.Common.Utility;
//using S3.Services.Record.Domain;
//using S3.Services.Record.Dto;
//using S3.Services.Record.Utility;

//namespace S3.Services.Record.StudentScores.Queries
//{
//    public class BrowseStudentScoresQueryHandler : IQueryHandler<BrowseStudentScoresQuery, IEnumerable<StudentScoreDto>>
//    {
//        private readonly IMapper _mapper;
//        private readonly RecordDbContext _db;

//        public BrowseStudentScoresQueryHandler(RecordDbContext db, IMapper mapper)
//            => (_db, _mapper) = (db, mapper);

//        public async Task<IEnumerable<StudentScoreDto>> HandleAsync(BrowseStudentScoresQuery query)
//        {
//            IQueryable<StudentScore> set = _db.StudentScores;

//            if (!(query.IncludeExpressions is null))
//                set = IncludeHelper<StudentScore>.IncludeComponents(set, query.IncludeExpressions);

//            set = query.SchoolId is null ?
//                set : set.Where(x => x.SchoolId == query.SchoolId);

//            set = query.ClassId is null ?
//                set : set.Where(x => x.ClassId == query.ClassId);

//            set = string.IsNullOrEmpty(query.Subject) ?
//                set : set.Where(x => x.Subject == query.Subject);

//            set = string.IsNullOrEmpty(query.ExamType) ?
//                set : set.Where(x => x.ExamType == query.ExamType);

//            set = query.Term is null ?
//                set : set.Where(x => x.Term == query.Term);

//            set = query.Session is null ?
//                set : set.Where(x => x.Session == query.Session);

//            var studentScores = _mapper.Map<IEnumerable<StudentScoreDto>>(set);

//            bool ascending = true;
//            if (!string.IsNullOrEmpty(query.SortOrder) &&
//                (query.SortOrder.ToLowerInvariant() == "desc" || query.SortOrder.ToLowerInvariant() == "descending"))
//            {
//                ascending = false;
//            }

//            if (!string.IsNullOrEmpty(query.OrderBy))
//            {
//                switch (query.OrderBy.ToLowerInvariant())
//                {
//                    case "createddate":
//                        studentScores = ascending ?
//                            studentScores.OrderBy(x => x.CreatedDate).ToList() :
//                            studentScores.OrderByDescending(x => x.CreatedDate).ToList();
//                        break;
//                    default:
//                        break;
//                }
//            }
//            else
//            {
//                studentScores = studentScores.OrderByDescending(x => x.UpdatedDate).ToList();
//            }

//            return studentScores;
//        }
//    }
//}

