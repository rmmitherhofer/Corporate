using AutoMapper;
using Candidatos.Api.DTO.Request;
using Candidatos.Business.Filters;
using Zypher.Persistence.Abstractions.Data.Filters;
using Zypher.Requests;

namespace Candidatos.Api.Configurations.AutoMapper;

public class FilterRequestToFilterProfile : Profile
{
    public FilterRequestToFilterProfile()
    {
        CreateMap<FilterRequest, Filter>();

        CreateMap<CandidatoFilterRequest, CandidatoFilter>();
    }
}
