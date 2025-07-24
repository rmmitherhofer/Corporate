using AutoMapper;
using PanPan.WebApp.Models.Response;
using PanPan.WebApp.Models.Response.Candidato;
using PanPan.WebApp.Models.Response.Cliente;
using PanPan.WebApp.Models.ViewModels;
using PanPan.WebApp.Models.ViewModels.Candidatos;
using PanPan.WebApp.Models.ViewModels.Clientes;
using Zypher.Extensions.Core;

namespace PanPan.WebApp.Configurations.AutoMapper;

public class MappingResponseToViewModelProfile : Profile
{
    public MappingResponseToViewModelProfile()
    {
        #region Shared
        CreateMap<PaginatedResponse, PageViewModel>();

        CreateMap<EnderecoResponse, EnderecoViewModel>();
        #endregion

        #region Candidato
        CreateMap<CandidatosPaginatedResponse, CandidatosViewModel>();

        CreateMap<CandidatoResponse, CandidatoViewModel>()
            .ForMember(d => d.DataNascimento, o => o.MapFrom(s => s.DataNascimento.HasValue ? s.DataNascimento.Value.ToString("dd/MM/yyyy") : null));

        CreateMap<FormacaoResponse, FormacaoViewModel>()
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFim))
            .ForMember(d => d.DataInicioString, o => o.MapFrom(s => s.DataInicio.HasValue ? s.DataInicio.Value.ToString("MM/yyyy") : null))
            .ForMember(d => d.DataFimString, o => o.MapFrom(s => s.DataFim.HasValue ? s.DataFim.Value.ToString("MM/yyyy") : null));

        CreateMap<ExperienciaResponse, ExperienciaViewModel>()
            .ForMember(d => d.UltimoSalario, o => o.MapFrom(s => s.UltimoSalario.ToPtBr()))
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFim))
            .ForMember(d => d.DataInicioString, o => o.MapFrom(s => s.DataInicio.ToString("MM/yyyy")))
            .ForMember(d => d.DataFimString, o => o.MapFrom(s => s.DataFim.HasValue ? s.DataFim.Value.ToString("MM/yyyy") : null));
        #endregion

        #region Cliente
        CreateMap<ClientesPaginatedResponse, ClientesViewModel>();

        CreateMap<ClienteResponse, ClienteViewModel>();
        #endregion




    }
}


