using PanPan.WebApp.Enums;

namespace PanPan.WebApp.Models;

public class CandidatoViewModel
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

    public IEnumerable<FormacaoViewModel>? Formacoes { get; set; }

    public string? Idiomas { get; set; }
    public string? Conhecimentos { get; set; }

    public RecrutadoPor? RecrutadoPor { get; set; }

    public string? RelatoCandidato { get; set; }
    public string? RelatoEntrevistador { get; set; }
    public string? Comentarios { get; set; }
    public StatusCandidato Status { get; set; }
    public List<string>? Tags { get; set; }
    public EnderecoViewModel? Endereco { get; set; }
    public IEnumerable<ExperienciaViewModel>? Experiencias { get; set; }
}
public class EnderecoViewModel
{
    public string? Logradouro { get; set; }
    public string? Numero { get; set; }
    public string? Complemento { get; set; }
    public string? Bairro { get; set; }
    public string? Cep { get; set; }
    public string? Cidade { get; set; }
    public string? Estado { get; set; }
    public string? MoraCom { get; set; }
    public Domicilio? Domicilio { get; set; }
    public TipoDomicilio? TipoDomicilio { get; set; }
}
public class ExperienciaViewModel
{
    public Guid Id { get; set; }
    public Guid CandidatoId { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string Empresa { get; set; }
    public string Cargo { get; set; }
    public string? Atividade { get; set; }
    public RegimeContratacao RegimeContratacao { get; set; }
    public string DataInicioString { get; set; }
    public string? DataFimString { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public StatusExperiencia Status { get; set; }
    public string? MotivoDesligamento { get; set; }
    public string? Observacoes { get; set; }

    public string? Habilidade { get; set; }
    public string? UltimoSalario { get; set; }
}
public class FormacaoViewModel
{
    public Guid Id { get; set; }
    public Guid CandidatoId { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }
    public string NomeInstituicao { get; set; }
    public string? Descricao { get; set; }
    public string? DataInicioString { get; set; }
    public string? DataFimString { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public TipoFormacao Tipo { get; set; }
    public Ensino? Ensino { get; set; }
    public Curso? Curso { get; set; }
    public StatusFormacao Status { get; set; }

    public PeriodoFormacao Periodo { get; set; }
}
