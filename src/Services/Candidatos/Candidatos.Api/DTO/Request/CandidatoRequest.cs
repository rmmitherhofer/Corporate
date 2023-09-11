using Candidatos.Business.Enums;

namespace Candidatos.Api.DTO.Request;

public abstract class CandidatoRequest
{
    public string Nome { get; set; }
    public Sexo Sexo { get; set; }
    public RecrutadoPor RecrutadoPor { get; set; }
    public string? Telefone { get; set; }
    public string? Celular { get; set; }
    public string? CelularAlternativo { get; set; }
    public string? Email { get; set; }
    public EnderecoRequest Endereco { get; set; }
}
public class AddCandidatoRequest : CandidatoRequest { }
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

public class EnderecoRequest
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

public abstract class FormacaoRequest
{
    public string NomeInstituicao { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public TipoFormacao Tipo { get; set; }
    public Ensino? Ensino { get; set; }
    public Curso? Curso { get; set; }
    public StatusFormacao Status { get; set; }
    public PeriodoFormacao Periodo { get; set; }
}
public class AddFormacaoRequest : FormacaoRequest { }
public class UpdateFormacaoRequest : FormacaoRequest { }

public abstract class ExperienciaRequest
{
    public string Empresa { get; set; }
    public string Cargo { get; set; }
    public string? Atividade { get; set; }
    public DateTime DataInicio { get; set; }
    public DateTime? DataFim { get; set; }
    public StatusExperiencia Status { get; set; }
    public RegimeContratacao RegimeContratacao { get; set; }
    public string? Observacoes { get; set; }
    public string? MotivoDesligamento { get; set; }
    public string? Habilidade { get; set; }
    public decimal? UltimoSalario { get; set; }
}
public class AddExperienciaRequest : ExperienciaRequest { }
public class UpdateExperienciaRequest : ExperienciaRequest { }