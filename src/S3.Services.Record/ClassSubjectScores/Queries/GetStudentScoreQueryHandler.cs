//using System.Linq;
//using System.Threading.Tasks;
//using AutoMapper;
//using Microsoft.EntityFrameworkCore;
//using S3.Common;
//using S3.Common.Handlers;
//using S3.Common.Mongo;
//using S3.Common.Utility;
//using S3.Services.Record.Domain;
//using S3.Services.Record.Dto;
//using S3.Services.Record.Utility;

//namespace S3.Services.Record.StudentScores.Queries
//{
//    public class GetStudentScoreQueryHandler : IQueryHandler<GetStudentScoreQuery, StudentScoreDto>
//    {
//        private readonly IMapper _mapper;
//        private readonly RecordDbContext _db;

//        public GetStudentScoreQueryHandler(RecordDbContext db, IMapper mapper)
//            => (_db, _mapper) = (db, mapper);

//        public async Task<StudentScoreDto> HandleAsync(GetStudentScoreQuery query)
//        {
//            IQueryable<StudentScore> set = _db.StudentScores;

//            if (!(query.IncludeExpressions is null))
//                set = IncludeHelper<StudentScore>.IncludeComponents(set, query.IncludeExpressions);

//            var studentScore = await set.FirstOrDefaultAsync(x => x.Id == query.Id);

//            return studentScore is null ? null! : _mapper.Map<StudentScoreDto>(studentScore);
//        }
//    }
//}