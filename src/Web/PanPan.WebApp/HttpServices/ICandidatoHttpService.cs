using PanPan.WebApp.Models.Request;
using PanPan.WebApp.Models.Response.Candidato;

namespace PanPan.WebApp.HttpServices;

public interface ICandidatoHttpService
{
    Task<IEnumerable<CandidatoResponse>> ObterTodos();
    Task<CandidatoResponse> ObterPorId(Guid id);
    Task<byte[]> GerarParecer(Guid id);

    Task Incluir(AddCandidatoRequest request);
    Task Alterar(Guid id, UpdateCandidatoRequest request);
    Task Remover(Guid id);

    Task Incluir(Guid id, AddFormacaoRequest request);
    Task Alterar(Guid id, Guid formacaoId, UpdateFormacaoRequest request);
    Task RemoverFormacao(Guid id, Guid formacaoId);

    Task Incluir(Guid id, AddExperienciaRequest request);
    Task Alterar(Guid id, Guid experienciaId, UpdateExperienciaRequest request);
    Task RemoverExperiencia(Guid id, Guid experienciaId);

}
