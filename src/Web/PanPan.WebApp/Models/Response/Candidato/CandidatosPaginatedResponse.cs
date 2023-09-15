namespace PanPan.WebApp.Models.Response.Candidato;

public class CandidatosPaginatedResponse : PaginatedResponse
{
    public IEnumerable<CandidatoResponse> Candidatos { get; set; }
}
