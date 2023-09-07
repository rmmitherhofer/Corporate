using Extensions;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Routing;
using PanPan.WebApp.Enums;
using PanPan.WebApp.Models;
using System;
using System.Text;

namespace PanPan.WebApp.Extensions;

public static class RazorExtensions
{
    private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "Files/Json/";

    public static string SelectOptionsEstados(this RazorPage page, string? estadoSelecionado = null) 
        => ObterEstados(estadoSelecionado);
    public static string SelectOptionsCidades(this RazorPage page, string? estadoSelecionado = null, string? cidadeSelecionada = null) 
        => ObterCidades(estadoSelecionado, cidadeSelecionada);
    
    public static string SelectOptionsFontesRecrutamento(this RazorPage page, RecrutadoPor? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<RecrutadoPor, string> dictionary = new();

        var type = typeof(RecrutadoPor);

        foreach (RecrutadoPor property in Enum.GetValues(type))        
            dictionary.Add(property, property.Descricao());  

        foreach (var item in dictionary.OrderBy(x => x.Value) ) {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsEstadoCivil(this RazorPage page, StatusCivil? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<StatusCivil, string> dictionary = new();

        var type = typeof(StatusCivil);

        foreach (StatusCivil property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsTipoFormacao(this RazorPage page, TipoFormacao? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<TipoFormacao, string> dictionary = new();

        var type = typeof(TipoFormacao);

        foreach (TipoFormacao property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsEnsino(this RazorPage page, Ensino? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<Ensino, string> dictionary = new();

        var type = typeof(Ensino);

        foreach (Ensino property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary)
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsCurso(this RazorPage page, Curso? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<Curso, string> dictionary = new();

        var type = typeof(Curso);

        foreach (Curso property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsStatusFormacao(this RazorPage page, StatusFormacao? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<StatusFormacao, string> dictionary = new();

        var type = typeof(StatusFormacao);

        foreach (StatusFormacao property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsStatusExperiencia(this RazorPage page, StatusExperiencia? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<StatusExperiencia, string> dictionary = new();

        var type = typeof(StatusExperiencia);

        foreach (StatusExperiencia property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }
    public static string SelectOptionsStatusCandidato(this RazorPage page, StatusCandidato? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<StatusCandidato, string> dictionary = new();

        var type = typeof(StatusCandidato);

        foreach (StatusCandidato property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }
    public static string SelectOptionsStatusRecrutamento(this RazorPage page, StatusRecrutamento? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<StatusRecrutamento, string> dictionary = new();

        var type = typeof(StatusRecrutamento);

        foreach (StatusRecrutamento property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }
    public static string SelectOptionsRegimeContratacao(this RazorPage page, RegimeContratacao? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<RegimeContratacao, string> dictionary = new();

        var type = typeof(RegimeContratacao);

        foreach (RegimeContratacao property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsDomicilio(this RazorPage page, Domicilio? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<Domicilio, string> dictionary = new();

        var type = typeof(Domicilio);

        foreach (Domicilio property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsPeriodoFormacao(this RazorPage page, PeriodoFormacao? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<PeriodoFormacao, string> dictionary = new();

        var type = typeof(PeriodoFormacao);

        foreach (PeriodoFormacao property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }

    public static string SelectOptionsTipoDomicilio(this RazorPage page, TipoDomicilio? valorSelecionado = null)
    {
        StringBuilder sb = new();
        Dictionary<TipoDomicilio, string> dictionary = new();

        var type = typeof(TipoDomicilio);

        foreach (TipoDomicilio property in Enum.GetValues(type))
            dictionary.Add(property, property.Descricao());

        foreach (var item in dictionary.OrderBy(x => x.Value))
        {
            var selecionado = "";

            if (item.Key == valorSelecionado)
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{item.Key}'>{item.Value}</option>");
        }
        return sb.ToString();
    }


    public static string ObterEstados(string? valorSelecionado)
    {
        StringBuilder sb = new();

        var model = ObterEstados();

        foreach (var estado in model.Estados.OrderBy(e => e.Nome))
        {
            var selecionado = "";

            if (estado.Sigla.Equals(valorSelecionado))
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{estado.Sigla}'>{estado.Nome}</option>");
        }
        return sb.ToString();
    }
    public static string ObterCidades(string? valorSelecionado1, string? valorSelecionado2)
    {
        StringBuilder sb = new();

        var model = ObterEstados();

        foreach (var cidade in model.Estados.Where(e => e.Sigla.Equals(valorSelecionado1)).Select(e => e.Cidades).SelectMany(c => c).OrderBy(c => c))
        {
            var selecionado = "";

            if (cidade.Equals(valorSelecionado2))
                selecionado = "selected=\"selected\"";

            sb.Append($"<option {selecionado} value='{cidade}'>{cidade}</option>");
        }
        return sb.ToString();
    }

    private static EstadosViewModel ObterEstados() 
        => FilesExtensions.JsonToObject<EstadosViewModel>(BaseDirectory + "cidades-brasil.json");
}