using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.Request.Candidato;

public class UpdateCandidatoRequest : CandidatoRequest
{
    public string? Apelido { get; set; }
    public DateTime? DataNascimento { get; set; }
    public int Idade { get; set; }
    public string? Naturalidade { get; set; }
    public StatusCivil? StatusCivil { get; set; }
    public string? Idiomas { get; set; }
    public string? Conhecimentos { get; set; }
    public string? Comentarios { get; set; }
    public StatusCandidato Status { get; set; }

    public string PretensaoSalarial { get; set; }
    public int QuantidadeFilhos { get; set; }
    public string? IdadeFilhos { get; set; }

    public string? RelatoCandidato { get; set; }
    public string? RelatoEntrevistador { get; set; }

    public string? CNH { get; set; }
}
