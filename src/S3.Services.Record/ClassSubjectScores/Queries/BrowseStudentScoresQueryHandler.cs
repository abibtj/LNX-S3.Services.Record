//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using S3.Common.Handlers;
//using S3.Common.Mongo;
//using S3.Common.Types;
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
//            var studentScores = _mapper.Map<IEnumerable<StudentScoreDto>>(_db.StudentScores.AsEnumerable());
           
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
//                    case "type":
//                        studentScores = ascending ?
//                            studentScores.OrderBy(x => x.Type).ToList() :
//                            studentScores.OrderByDescending(x => x.Type).ToList();
//                        break;
//                    case "term":
//                        studentScores = ascending ?
//                            studentScores.OrderBy(x => x.Term).ToList() :
//                            studentScores.OrderByDescending(x => x.Term).ToList();
//                        break;
//                    case "session":
//                        studentScores = ascending ?
//                            studentScores.OrderBy(x => x.Session).ToList() :
//                            studentScores.OrderByDescending(x => x.Session).ToList();
//                        break;
//                    default:
//                        break;
//                }
//            }
//            return studentScores;
//        }
//    }
//}
