using PanPan.WebApp.Models.Request.Candidato;
using PanPan.WebApp.Models.Response.Candidato;
using PanPan.WebApp.Models.ViewModels.Candidatos;

namespace PanPan.WebApp.HttpServices;

public interface ICandidatoHttpService
{
    Task<CandidatosPaginatedResponse> ObterPaginado(CandidatosViewModel request);
    Task<CandidatoResponse> ObterPorId(Guid id);
    Task<byte[]> GerarParecer(Guid id, string resp);

    Task Adicionar(AddCandidatoRequest request);
    Task Alterar(Guid id, UpdateCandidatoRequest request);
    Task Remover(Guid id);

    Task<IEnumerable<FormacaoResponse>> ObterFormacoes(Guid candidatoId);
    Task Adicionar(Guid id, AddFormacaoRequest request);
    Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request);
    Task RemoverFormacao(Guid id, Guid formacaoId);

    Task<IEnumerable<ExperienciaResponse>> ObterExperiencias(Guid candidatoId);
    Task Adicionar(Guid id, AddExperienciaRequest request);
    Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request);
    Task RemoverExperiencia(Guid id, Guid experienciaId);

}
