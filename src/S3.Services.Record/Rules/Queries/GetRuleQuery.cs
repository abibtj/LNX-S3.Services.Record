using System;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using S3.Common.Types;
using S3.Services.Record.Dto;

namespace S3.Services.Record.Rules.Queries
{
    public class GetRuleQuery : IQuery<RuleDto>
    {
        public Guid Id { get; }

        [JsonConstructor]
        public GetRuleQuery(Guid id) => Id = id;
    }
}