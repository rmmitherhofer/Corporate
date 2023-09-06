using Api.Core.Data.Filters;
using AutoMapper;
using Candidatos.Api.DTO.Request;
using Candidatos.Business.Filters;

namespace Candidatos.Api.Configurations.AutoMapper;

public class FilterRequestToFilterProfile : Profile
{
    public FilterRequestToFilterProfile()
    {
        CreateMap<FilterRequest, Filter>();

        CreateMap<CandidatoFilterRequest, CandidatoFilter>();
    }
}
