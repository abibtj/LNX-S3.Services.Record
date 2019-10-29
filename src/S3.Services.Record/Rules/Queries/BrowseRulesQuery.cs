using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;
using System;
using System.Collections.Generic;

namespace S3.Services.Record.Rules.Queries
{
    public class BrowseRulesQuery : BrowseQuery<Rule>, IQuery<IEnumerable<RuleDto>>
    {
        public Guid? SchoolId { get; set; }

        [JsonConstructor]
        public BrowseRulesQuery(string[]? includeArray, Guid? schoolId,int page, int results, string orderBy, string sortOrder)
          : base(includeArray, page, results, orderBy, sortOrder)

            => SchoolId = schoolId;
    }
}