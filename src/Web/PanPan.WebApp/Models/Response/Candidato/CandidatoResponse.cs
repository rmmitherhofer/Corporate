using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.Response.Candidato;

public class CandidatoResponse
{
    public Guid Id { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public int Codigo { get; set; }
    public string Nome { get; set; }
    public Sexo Sexo { get; set; }
    public string? Apelido { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? CelularAlternativo { get; set; }
    public string? Email { get; set; }

    public DateTime? DataNascimento { get; set; }
    public int Idade { get; set; }
    public string? Naturalidade { get; set; }
    public StatusCivil? StatusCivil { get; set; }

    public string PretensaoSalarial { get; set; }
    public int QuantidadeFilhos { get; set; }
    public string? IdadeFilhos { get; set; }

    public IEnumerable<FormacaoResponse>? Formacoes { get; set; }

    public string? Idiomas { get; set; }
    public string? Conhecimentos { get; set; }

    public RecrutadoPor? RecrutadoPor { get; set; }

    public string? RelatoCandidato { get; set; }
    public string? RelatoEntrevistador { get; set; }
    public string? Comentarios { get; set; }
    public string? CNH { get; set; }
    public StatusCandidato Status { get; set; }
    public List<string>? Tags { get; set; }
    public EnderecoResponse? Endereco { get; set; }
    public IEnumerable<ExperienciaResponse>? Experiencias { get; set; }
}
