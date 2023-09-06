using AutoMapper;
using Extensions;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Response.Candidato;

namespace PanPan.WebApp.Configurations.AutoMapper;

public class MappingResponseToViewModelProfile : Profile
{
    public MappingResponseToViewModelProfile()
    {
        CreateMap<CandidatoResponse, CandidatoViewModel>()
            .ForMember(d => d.DataNascimento, o => o.MapFrom(s => s.DataNascimento.HasValue ? s.DataNascimento.Value.ToString("dd/MM/yyyy") : null));

        CreateMap<EnderecoResponse, EnderecoViewModel>();

        CreateMap<FormacaoResponse, FormacaoViewModel>()
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFim))
            .ForMember(d => d.DataInicioString, o => o.MapFrom(s => s.DataInicio.HasValue ? s.DataInicio.Value.ToString("MM/yyyy") : null))
            .ForMember(d => d.DataFimString, o => o.MapFrom(s => s.DataFim.HasValue ? s.DataFim.Value.ToString("MM/yyyy") : null));

        CreateMap<ExperienciaResponse, ExperienciaViewModel>()
            .ForMember(d => d.UltimoSalario, o => o.MapFrom(s => s.UltimoSalario.PtBR()))
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFim))
            .ForMember(d => d.DataInicioString, o => o.MapFrom(s => s.DataInicio.ToString("MM/yyyy")))
            .ForMember(d => d.DataFimString, o => o.MapFrom(s => s.DataFim.HasValue ? s.DataFim.Value.ToString("MM/yyyy") : null));
    }
}


