using S3.Common.Types;
using S3.Services.Record.Dto;
using System.Collections.Generic;

namespace S3.Services.Record.Rules.Queries
{
    public class BrowseRulesQuery : PagedQueryBase, IQuery<IEnumerable<RuleDto>>
    {
    }
}