using Candidatos.Business.Enums;
using Core.DomainObjects;
using Exceptions;

namespace Candidatos.Business.Models;

public class Candidato : Entity, IAggregateRoot
{
    public int Codigo { get; private set; }
    public string Nome { get; private set; }
    public string? Apelido { get; private set; }
    public string? Telefone { get; private set; }
    public string? Celular { get; private set; }
    public string? CelularAlternativo { get; private set; }
    public string? Email { get; private set; }

    public DateTime? DataNascimento { get; private set; }
    public int Idade { get; private set; }
    public string? Naturalidade { get; private set; }
    public StatusCivil? StatusCivil { get; private set; }

    public string? PretensaoSalarial { get; private set; }
    public int QuantidadeFilhos { get; private set; }
    public string? IdadeFilhos { get; private set; }

    private List<Formacao>? _formacoes;
    public IReadOnlyCollection<Formacao>? Formacoes => _formacoes?.AsReadOnly();

    public string? Idiomas { get; private set; }
    public string? Conhecimentos { get; private set; }
    public RecrutadoPor? RecrutadoPor { get; private set; }


    public StatusCandidato Status { get; private set; }
    public List<string>? Tags { get; private set; }
    public Endereco? Endereco { get; private set; }

    private List<Experiencia>? _experiencias;
    public IReadOnlyCollection<Experiencia>? Experiencias => _experiencias?.AsReadOnly();

    private List<Historico>? _historico;
    public IReadOnlyCollection<Historico>? Historico => _historico?.AsReadOnly();

    public Sexo Sexo { get; private set; }
    public string? RelatoCandidato { get; private set; }
    public string? RelatoEntrevistador { get; private set; }
    public string? Comentarios { get; private set; }
    public string? CNH { get; private set; }

    protected Candidato() { }

    public Candidato(string nome, Sexo sexo, RecrutadoPor recrutadoPor, string? telefone, string? celular, string? celularAlternativo, string? email)
    {
        Sexo = sexo;
        AtribuirNome(nome);
        AtribuirDadosContato(telefone, celular, celularAlternativo, email);

        RecrutadoPor = recrutadoPor;
        Status = StatusCandidato.Triagem;

        var arrayNome = Nome.Split(" ");

        Apelido = arrayNome[0] + " " + arrayNome[new Random().Next(1, arrayNome.Length - 1)];

        _experiencias = new();
        _formacoes = new();
        Tags = new();
    }
    public void AtribuirNome(string nome)
    {
        if (string.IsNullOrEmpty(nome)) throw new DomainException("O nome do candidato precisa ser informado.");

        if (nome.Split(" ").Length <= 1) throw new DomainException("O nome e sobrenome devem ser informados.");

        Nome = nome.Trim();
    }
    public void AlterarApelido(string? apelido) => Apelido = apelido?.Trim();
    public void AtribuirDadosContato(string? telefone, string? celular, string? celularAlternativo, string? emails)
    {
        if (string.IsNullOrEmpty(telefone) && string.IsNullOrEmpty(celular) && string.IsNullOrEmpty(celularAlternativo) && string.IsNullOrEmpty(emails))
            throw new DomainException("Ao menos um dado de contato entre telefone, celular, celular alternativo ou e-mail precisa ser preenchido");

        Telefone = telefone?.Trim();
        Celular = celular?.Trim();
        CelularAlternativo = celularAlternativo?.Trim();
        Core.ValueObjects.Email.SaoValidos(emails);
        Email = emails?.Trim();
    }
    public void AtribuirDadosPessoais(DateTime? dataNascimento, int? idade, string? naturalidade, StatusCivil? statusCivil)
    {
        DataNascimento = dataNascimento;
        Idade = IdadeEmAnos(idade);
        Naturalidade = naturalidade?.Trim();
        StatusCivil = statusCivil;
    }
    public void AtribuirDadosComplementares(string? idiomas, string? conhecimentos, string pretensaoSalarial, string? idadeFilhos, int quantidadeFilhos = 0)
    {
        Idiomas = idiomas?.Trim();
        Conhecimentos = conhecimentos?.Trim();
        PretensaoSalarial = pretensaoSalarial.Trim();
        QuantidadeFilhos = quantidadeFilhos;
        IdadeFilhos = idadeFilhos?.Trim();
    }
    public void IncluirComentarios(string? comentarios) => Comentarios = comentarios?.Trim();

    public void IncluirRelatos(string? relatosCandidato, string? relatosEntrevistador)
    {
        RelatoCandidato = relatosCandidato?.Trim();
        RelatoEntrevistador = relatosEntrevistador?.Trim();
    }

    public void AtribuirCNH(string? cnh) => CNH = cnh?.Trim();
    public void AlterarStatus(StatusCandidato status) => Status = status;
    public void IncluirTag(string tag)
    {
        Tags ??= new List<string>();

        Tags.Add(tag.Trim());
    }
    public void RemoverTag(string tag)
    {
        Tags ??= new List<string>();

        Tags.Remove(tag.Trim());
    }
    public void IncluirEndereco(Endereco endereco)
    {
        Endereco = endereco;
        Endereco?.AtribuirCandidato(Id);
    }
    public void AlterarEndereco(Endereco endereco)
    {
        if (Endereco is null)
        {
            IncluirEndereco(endereco);
            return;
        }
        Endereco?.Alterar(endereco);
    }

    private int IdadeEmAnos(int? anos)
    {
        if (DataNascimento is null)
        {
            return !anos.HasValue || anos.Value <= 0
                ? throw new DomainException("A Idade ou a data de nascimento precisa ser preenchida.")
                : anos.Value;
        }

        var now = DateTime.Now;

        int yearsOld = now.Year - DataNascimento.Value.Year;
        if (now.Month < DataNascimento.Value.Month || now.Month == DataNascimento.Value.Month && now.Day < DataNascimento.Value.Day)
            yearsOld--;

        return yearsOld < 0 ? 0 : yearsOld;
    }
}
