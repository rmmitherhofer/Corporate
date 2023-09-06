using Candidatos.Business.Enums;

namespace Candidatos.Business.Models;

public class Endereco
{
    public string? Logradouro { get; private set; }
    public string? Numero { get; private set; }
    public string? Complemento { get; private set; }
    public string? Bairro { get; private set; }
    public string? Cep { get; private set; }
    public string? Cidade { get; private set; }
    public string? Estado { get; private set; }
    public string? MoraCom { get; private set; }
    public Domicilio? Domicilio { get; private set; }
    public TipoDomicilio? TipoDomicilio { get; private set; }
    public Guid CandidatoId { get; private set; }

    protected Endereco() { }

    public Endereco(string? bairro, string?  cidade, string? estado)
    {
        Bairro = bairro;
        Cidade = cidade;
        Estado = estado;
    }
    public Endereco(string? logradouro, string? numero, string? complemento, string? bairro, string? cep, string? cidade, string? estado, Domicilio? domicilio, TipoDomicilio? tipoDomicilio, string? moraCom)
    {
        Logradouro = logradouro;
        Numero = numero;
        Complemento = complemento;
        Bairro = bairro;
        Cep = cep;
        Cidade = cidade;
        Estado = estado;
        Domicilio = domicilio;
        TipoDomicilio = tipoDomicilio;
        MoraCom = moraCom;
    }

    public void AtribuirCandidato(Guid candidatoId) => CandidatoId = candidatoId;
    public void Alterar(Endereco endereco)
    {
        Logradouro = endereco.Logradouro;
        Numero = endereco.Numero;
        Complemento = endereco.Complemento;
        Bairro = endereco.Bairro;
        Cep = endereco.Cep;
        Cidade = endereco.Cidade;
        Estado = endereco.Estado;
        Domicilio = endereco.Domicilio;
        TipoDomicilio = endereco.TipoDomicilio;
        MoraCom = endereco.MoraCom;
    }
}
