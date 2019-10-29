using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Common.Utility;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;
using S3.Services.Record.Utility;

namespace S3.Services.Record.ClassSubjectScores.Queries
{
    public class GetClassSubjectScoresQueryHandler : IQueryHandler<GetClassSubjectScoresQuery, ClassSubjectScoresDto>
    {
        private readonly IMapper _mapper;
        private readonly RecordDbContext _db;

        public GetClassSubjectScoresQueryHandler(RecordDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<ClassSubjectScoresDto> HandleAsync(GetClassSubjectScoresQuery query)
        {
            IQueryable<Domain.ClassSubjectScores> set = _db.ClassSubjectScores;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<Domain.ClassSubjectScores>.IncludeComponents(set, query.IncludeExpressions);

            var classSubjectScores = await set.FirstOrDefaultAsync(x => x.Id == query.Id);

            return classSubjectScores is null ? null! : _mapper.Map<ClassSubjectScoresDto>(classSubjectScores);
        }
    }
}