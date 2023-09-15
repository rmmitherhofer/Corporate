using AutoMapper;
using Extensions;
using PanPan.WebApp.Models.Request;
using PanPan.WebApp.Models.Request.Candidato;
using PanPan.WebApp.Models.Request.Cliente;
using PanPan.WebApp.Models.ViewModels;
using PanPan.WebApp.Models.ViewModels.Candidatos;
using PanPan.WebApp.Models.ViewModels.Clientes;

namespace PanPan.WebApp.Configurations.AutoMapper;

public class MappingViewModelToRequestProfile : Profile
{
    public MappingViewModelToRequestProfile()
    {
        #region Shared
        CreateMap<EnderecoViewModel, EnderecoRequest>(); 
        #endregion

        #region Candidato
        CreateMap<CandidatoViewModel, AddCandidatoRequest>();

        CreateMap<CandidatoViewModel, UpdateCandidatoRequest>()
            .ForMember(d => d.DataNascimento, o => o.MapFrom(s => s.DataNascimento.DatePtBRToDateTimeNulable()));

        CreateMap<FormacaoViewModel, AddFormacaoRequest>()
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicioString.MesAnoPtBRToDateTimeNulable()))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFimString.MesAnoPtBRToDateTimeNulable()));

        CreateMap<FormacaoViewModel, UpdateFormacaoRequest>()
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicioString.MesAnoPtBRToDateTimeNulable()))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFimString.MesAnoPtBRToDateTimeNulable()));

        CreateMap<ExperienciaViewModel, AddExperienciaRequest>()
            .ForMember(d => d.UltimoSalario, o => o.MapFrom(s => s.UltimoSalario.ReverterMascaraParaDecimal(null)))
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicioString.MesAnoPtBRToDateTime()))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFimString.MesAnoPtBRToDateTimeNulable()));

        CreateMap<ExperienciaViewModel, UpdateExperienciaRequest>()
            .ForMember(d => d.UltimoSalario, o => o.MapFrom(s => s.UltimoSalario.ReverterMascaraParaDecimal(null)))
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicioString.MesAnoPtBRToDateTime()))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFimString.MesAnoPtBRToDateTimeNulable()));
        #endregion

        #region Cliente
        CreateMap<ClienteViewModel, AddClienteRequest>();

        CreateMap<ClienteViewModel, UpdateClienteRequest>();
        #endregion
    }
}


