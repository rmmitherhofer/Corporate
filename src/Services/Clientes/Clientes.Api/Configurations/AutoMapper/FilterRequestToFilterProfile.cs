using Api.Core.Data.Filters;
using AutoMapper;
using Clientes.Api.Dto.Request;
using Clientes.Business.Filters;
using Requests;

namespace Clientes.Api.Configurations.AutoMapper;

public class FilterRequestToFilterProfile : Profile
{
    public FilterRequestToFilterProfile()
    {
        CreateMap<FilterRequest, Filter>();

        CreateMap<ClienteFilterRequest, ClienteFilter>();
    }
}
