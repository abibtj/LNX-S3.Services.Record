using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using S3.Common.Handlers;
using S3.Common.Mongo;
using S3.Common.Types;
using S3.Common.Utility;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;
using S3.Services.Record.Utility;

namespace S3.Services.Record.Rules.Queries
{
    public class BrowseRulesQueryHandler : IQueryHandler<BrowseRulesQuery, IEnumerable<RuleDto>>
    {
        private readonly IMapper _mapper;
        private readonly RecordDbContext _db;

        public BrowseRulesQueryHandler(RecordDbContext db, IMapper mapper)
            => (_db, _mapper) = (db, mapper);

        public async Task<IEnumerable<RuleDto>> HandleAsync(BrowseRulesQuery query)
        {
            IQueryable<Rule> set = _db.Rules;

            if (!(query.IncludeExpressions is null))
                set = IncludeHelper<Rule>.IncludeComponents(set, query.IncludeExpressions);

            set = query.SchoolId is null ?
                set : set.Where(x => x.SchoolId == query.SchoolId);

            var rules = _mapper.Map<IEnumerable<RuleDto>>(set);

            bool ascending = true;
            if (!string.IsNullOrEmpty(query.SortOrder) &&
                (query.SortOrder.ToLowerInvariant() == "desc" || query.SortOrder.ToLowerInvariant() == "descending"))
            {
                ascending = false;
            }

            if (!string.IsNullOrEmpty(query.OrderBy))
            {
                switch (query.OrderBy.ToLowerInvariant())
                {
                    case "name":
                        rules = ascending ?
                            rules.OrderBy(x => x.Name).ToList() :
                            rules.OrderByDescending(x => x.Name).ToList();
                        break;
                    case "createddate":
                        rules = ascending ?
                            rules.OrderBy(x => x.CreatedDate).ToList() :
                            rules.OrderByDescending(x => x.CreatedDate).ToList();
                        break;
                    default:
                        break;
                }
            }
            return rules;
        }
    }
}
