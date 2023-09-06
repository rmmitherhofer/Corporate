using AutoMapper;
using Extensions;
using PanPan.WebApp.Models;
using PanPan.WebApp.Models.Request;

namespace PanPan.WebApp.Configurations.AutoMapper;

public class MappingViewModelToRequestProfile : Profile
{
    public MappingViewModelToRequestProfile()
    {
        CreateMap<CandidatoViewModel, AddCandidatoRequest>();

        CreateMap<CandidatoViewModel, UpdateCandidatoRequest>()
            .ForMember(d => d.DataNascimento, o => o.MapFrom(s => s.DataNascimento.DatePtBRToDateTimeNulable()));

        CreateMap<EnderecoViewModel, EnderecoRequest>();

        CreateMap<FormacaoViewModel, AddFormacaoRequest>()
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio.MesAnoPtBRToDateTimeNulable()))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFim.MesAnoPtBRToDateTimeNulable()));

        CreateMap<FormacaoViewModel, UpdateFormacaoRequest>()
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio.MesAnoPtBRToDateTimeNulable()))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFim.MesAnoPtBRToDateTimeNulable()));

        CreateMap<ExperienciaViewModel, AddExperienciaRequest>()
            .ForMember(d => d.UltimoSalario, o => o.MapFrom(s => s.UltimoSalario.ReverterMascaraParaDecimal(null)))
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio.MesAnoPtBRToDateTime()))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFim.MesAnoPtBRToDateTimeNulable()));

        CreateMap<ExperienciaViewModel, UpdateExperienciaRequest>()
            .ForMember(d => d.UltimoSalario, o => o.MapFrom(s => s.UltimoSalario.ReverterMascaraParaDecimal(null)))
            .ForMember(d => d.DataInicio, o => o.MapFrom(s => s.DataInicio.MesAnoPtBRToDateTime()))
            .ForMember(d => d.DataFim, o => o.MapFrom(s => s.DataFim.MesAnoPtBRToDateTimeNulable()));

    }
}


