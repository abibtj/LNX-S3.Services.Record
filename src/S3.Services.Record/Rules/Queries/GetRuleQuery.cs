using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;

namespace S3.Services.Record.Rules.Queries
{
    public class GetRuleQuery : GetQuery<Rule>, IQuery<RuleDto>
    {
        [JsonConstructor]
        public GetRuleQuery(Guid id, string[]? includeArray) : base(id, includeArray) { }
    }
}