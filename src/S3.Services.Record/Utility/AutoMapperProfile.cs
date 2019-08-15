using AutoMapper;
using S3.Services.Record.Domain;
using S3.Services.Record.Dto;

namespace S3.Services.Record.Utility
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Rule, RuleDto>().ReverseMap();
        }
    }
}
