using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models.ViewModels.Candidatos;
public class CandidatoViewModel : ViewModel
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

    public string? DataNascimento { get; set; }
    public int Idade { get; set; }
    public string? Naturalidade { get; set; }
    public StatusCivil? StatusCivil { get; set; }

    public string PretensaoSalarial { get; set; } = "0";
    public int QuantidadeFilhos { get; set; }
    public string? IdadeFilhos { get; set; }

    public List<FormacaoViewModel>? Formacoes { get; set; }

    public string? Idiomas { get; set; }
    public string? Conhecimentos { get; set; }

    public RecrutadoPor? RecrutadoPor { get; set; }

    public string? RelatoCandidato { get; set; }
    public string? RelatoEntrevistador { get; set; }
    public string? Comentarios { get; set; }
    public StatusCandidato Status { get; set; }
    public string? CNH { get; set; }
    public List<string>? Tags { get; set; }
    public EnderecoViewModel? Endereco { get; set; }
    public List<ExperienciaViewModel>? Experiencias { get; set; }
    public IFormFile File { get; set; }

}

public class JsonResumeImport
{
    public string Json { get; set; }
}