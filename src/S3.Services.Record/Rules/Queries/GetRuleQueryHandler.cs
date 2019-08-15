using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;
using S3.Services.Record.Utility;

namespace S3.Services.Record.Rules.Queries
{
    public class GetRuleQueryHandler : IQueryHandler<GetRuleQuery, RuleDto>
    {
        private readonly IMapper _mapper;
        private readonly RecordDbContext _db;

        public GetRuleQueryHandler(RecordDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<RuleDto> HandleAsync(GetRuleQuery query)
        {
            var rule = await _db.Rules.FirstOrDefaultAsync(x => x.Id == query.Id);

            return rule is null? null! : _mapper.Map<RuleDto>(rule);
        }
    }
}