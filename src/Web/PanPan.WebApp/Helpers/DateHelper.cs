using PanPan.WebApp.Enums;
using PanPan.WebApp.Models.ViewModels;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using Zypher.Json;

namespace PanPan.WebApp.Helpers;

public static class DateHelper
{
    public static DateTime? TryParseToFirstDayOfMonth(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        input = input.Trim().ToLowerInvariant();

        var months = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase)
        {
            ["janeiro"] = 1,
            ["fevereiro"] = 2,
            ["março"] = 3,
            ["abril"] = 4,
            ["maio"] = 5,
            ["junho"] = 6,
            ["julho"] = 7,
            ["agosto"] = 8,
            ["setembro"] = 9,
            ["outubro"] = 10,
            ["novembro"] = 11,
            ["dezembro"] = 12
        };

        string[] formats = [
            "MM/yyyy", "M/yyyy", "yyyy-MM", "yyyy/MM", "yyyy",
            "dd/MM/yyyy", "d/M/yyyy", "MM/yy", "M/yy", "yy-MM", "yyyyMMdd",
            "MMMM yyyy", "MMMM 'de' yyyy", "MMM yyyy"
        ];

        if (DateTime.TryParseExact(input, formats, new CultureInfo("pt-BR"), DateTimeStyles.None, out var parsed))
            return new DateTime(parsed.Year, parsed.Month, 1);

        foreach (var month in months)
        {
            if (input.Contains(month.Key))
            {
                var matchYear = Regex.Match(input, @"\d{4}");
                if (matchYear.Success)
                {
                    if (int.TryParse(matchYear.Value, out int year))
                        return new DateTime(year, month.Value, 1);
                }
            }
        }
        return null;
    }
}

public static class CourseClassifier
{
    public static Ensino? ClassificarEnsino(string nomeInstituicao, string descricaoCurso)
    {
        nomeInstituicao = nomeInstituicao?.ToLower() ?? "";
        descricaoCurso = descricaoCurso?.ToLower() ?? "";

        // Superior Bacharelado (ex: Engenharia, Bacharelado)
        string[] palavrasSuperiorBacharelado = { "engenharia", "bacharelado", "medicina", "direito", "administração" };
        if (palavrasSuperiorBacharelado.Any(p => nomeInstituicao.Contains(p) || descricaoCurso.Contains(p)))
            return Ensino.SuperiorBacharelado;

        // Superior Licenciatura (ex: Pedagogia, Licenciatura)
        string[] palavrasSuperiorLicenciatura = { "licenciatura", "pedagogia", "professor" };
        if (palavrasSuperiorLicenciatura.Any(p => nomeInstituicao.Contains(p) || descricaoCurso.Contains(p)))
            return Ensino.SuperiorLicenciatura;

        // Superior Tecnólogo (ex: Tecnólogo, Tecnologia)
        string[] palavrasSuperiorTecnologo = { "tecnólogo", "tecnologia", "tecno" };
        if (palavrasSuperiorTecnologo.Any(p => nomeInstituicao.Contains(p) || descricaoCurso.Contains(p)))
            return Ensino.SuperiorTecnologo;

        // Pós-graduação (ex: pós-graduação, especialização, MBA, mestrado, doutorado)
        string[] palavrasPosGraduacao = { "pós-graduação", "especialização", "mba" };
        if (palavrasPosGraduacao.Any(p => nomeInstituicao.Contains(p) || descricaoCurso.Contains(p)))
            return Ensino.PosGraduacao;

        // Mestrado e Doutorado
        if (descricaoCurso.Contains("mestrado"))
            return Ensino.Mestrado;
        if (descricaoCurso.Contains("doutorado"))
            return Ensino.Doutorado;

        // Ensino Técnico ou Médio-Técnico
        string[] palavrasMedioTecnico = { "médio técnico", "ensino médio técnico" };
        if (palavrasMedioTecnico.Any(p => nomeInstituicao.Contains(p) || descricaoCurso.Contains(p)))
            return Ensino.MedioTecnico;

        string[] palavrasTecnico = { "técnico", "curso técnico", "profissionalizante" };
        if (palavrasTecnico.Any(p => nomeInstituicao.Contains(p) || descricaoCurso.Contains(p)))
            return Ensino.Tecnico;

        // Ensino médio
        string[] palavrasMedio = { "ensino médio", "médio" };
        if (palavrasMedio.Any(p => nomeInstituicao.Contains(p) || descricaoCurso.Contains(p)))
            return Ensino.Medio;

        // Ensino fundamental
        string[] palavrasFundamental = { "fundamental", "ensino fundamental" };
        if (palavrasFundamental.Any(p => nomeInstituicao.Contains(p) || descricaoCurso.Contains(p)))
            return Ensino.Fundamental;

        // Default null (não identificado)
        return null;
    }

    public static Curso? ClassificarCurso(string nomeInstituicao, string descricaoCurso)
    {
        nomeInstituicao = nomeInstituicao?.ToLower() ?? "";
        descricaoCurso = descricaoCurso?.ToLower() ?? "";

        if ((nomeInstituicao.Contains("técnico") || descricaoCurso.Contains("técnico")) &&
            !descricaoCurso.Contains("superior")) // evitar conflito com ensino superior tecnólogo
            return Curso.Tecnico;

        if (descricaoCurso.Contains("profissionalizante") || descricaoCurso.Contains("curso profissionalizante"))
            return Curso.Profissionalizante;

        return Curso.Complementar;
    }
}

public static class CNHNormalizer
{
    private static readonly List<string> AllowedCategories = ["A", "B", "C", "D", "E"];

    public static string NormalizeCNH(string raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return "Nao possui";

        // Remove tudo que não for letra ou vírgula (para preservar múltiplas categorias)
        var cleaned = Regex.Replace(raw.ToUpper(), @"[^A-E,]", "");

        // Extrai letras válidas
        var foundLetters = new HashSet<string>();
        foreach (Match m in Regex.Matches(cleaned, "[A-E]"))
        {
            foundLetters.Add(m.Value);
        }

        if (foundLetters.Count == 0)
            return "Nao possui";

        // Ordena as categorias
        var ordered = AllowedCategories.Where(c => foundLetters.Contains(c)).ToList();

        // Monta string no padrão das opções
        return JoinCategories(ordered);
    }

    private static string JoinCategories(List<string> categories)
    {
        if (categories.Count == 1)
            return categories[0];

        if (categories.Count == 2)
            return $"{categories[0]} e {categories[1]}";

        // 3 ou mais: "A, B, C e D"
        var allButLast = categories.Take(categories.Count - 1);
        var last = categories.Last();

        return $"{string.Join(", ", allButLast)} e {last}";
    }
}

public class NomesPorSexo
{
    public List<string> Masculino { get; set; } = [];
    public List<string> Feminino { get; set; } = [];
}

public static class SexoExtensions
{
    public static Sexo? ParseSexo(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        var normalized = input.Trim().ToLowerInvariant();

        if (normalized is "masculino" or "m" or "male")
            return Sexo.Masculino;

        if (normalized is "feminino" or "f" or "female" or "fem")
            return Sexo.Feminino;

        return null;
    }

    public static Sexo? InferirSexoPorNome(string? nomeCompleto)
    {
        if (string.IsNullOrWhiteSpace(nomeCompleto))
            return null;

        var nomesPorSexo = JsonFileExtensions.JsonToObject<NomesPorSexo>(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files/Json/names-by-gender.json"));

        var primeiroNome = nomeCompleto.Split(' ', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        if (string.IsNullOrWhiteSpace(primeiroNome))
            return null;

        if (nomesPorSexo.Masculino.Contains(primeiroNome))
            return Sexo.Masculino;

        if (nomesPorSexo.Feminino.Contains(primeiroNome))
            return Sexo.Feminino;

        return null;
    }

    public static Sexo ParseOuInferirSexo(string? sexoInput, string? nomeCompleto) 
        => ParseSexo(sexoInput) ?? InferirSexoPorNome(nomeCompleto) ?? Sexo.Masculino;
}
public static class TelefoneHelper
{
    public static (TipoTelefone tipo, string numeroFormatado) PadronizarTelefone(string? raw)
    {
        if (string.IsNullOrWhiteSpace(raw))
            return (TipoTelefone.Desconhecido, null);

        // Remove tudo que não for dígito
        var digits = new string(raw.Where(char.IsDigit).ToArray());

        if (digits.Length < 10)
            return (TipoTelefone.Desconhecido, raw);

        // DDD + Número
        string ddd = digits.Substring(0, 2);
        string numero = digits.Substring(2);

        if (numero.Length == 9 && numero.StartsWith('9'))
        {
            // Celular padrão novo
            return (TipoTelefone.Celular, $"({ddd}) {numero.Substring(0, 1)}{numero.Substring(1, 4)}-{numero.Substring(5)}");
        }
        else if (numero.Length == 8)
        {
            // Telefone fixo
            return (TipoTelefone.Fixo, $"({ddd}) {numero.Substring(0, 4)}-{numero.Substring(4)}");
        }

        // Caso não encaixe, retorna original
        return (TipoTelefone.Desconhecido, raw);
    }
}

public enum TipoTelefone
{
    Celular,
    Fixo,
    Desconhecido
}

public static class EstadoCivilHelper
{
    public static StatusCivil MapearEstadoCivil(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return StatusCivil.Indefinido;

        string valor = input.Trim().ToLowerInvariant();

        if (valor.Contains("solteir") || valor.Contains("single"))
            return StatusCivil.Solteiro;

        if (valor.Contains("casad") || valor.Contains("married"))
            return StatusCivil.Casado;

        if (valor.Contains("separad") || valor.Contains("separated"))
            return StatusCivil.Separado;

        if (valor.Contains("divorciad") || valor.Contains("divorced"))
            return StatusCivil.Divorciado;

        if (valor.Contains("viu") || valor.Contains("widow") || valor.Contains("widower"))
            return StatusCivil.Viuvo;

        return StatusCivil.Indefinido;
    }
}

public static class IdadeHelper
{
    public static int MapearIdade(string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return 0;

        // Remove espaços e letras desnecessárias
        string texto = input.Trim().ToLowerInvariant();

        // Regex para extrair número (idade)
        var match = Regex.Match(texto, @"\b(\d{1,3})\b");
        if (match.Success && int.TryParse(match.Groups[1].Value, out int idade))
        {
            if (idade >= 0 && idade <= 120) // faixa válida
                return idade;
        }

        return 0;
    }
}

public class EnderecoCorrigido
{
    public string Estado { get; set; } // Sigla (ex: SP)
    public string Cidade { get; set; }
    public string Bairro { get; set; }
}

public class EnderecoCorretor
{
    private readonly List<EstadoViewModel> _estados;
    private readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory + "Files/Json/";


    public EnderecoCorretor()
    {
        _estados = JsonFileExtensions.JsonToObject<EstadosViewModel>(BaseDirectory + "cidades-brasil.json").Estados.ToList();
    }

    public EnderecoCorrigido CorrigirEndereco(string estadoInput, string cidadeInput, string bairroInput)
    {
        var todosNomes = new List<string?> { estadoInput, cidadeInput, bairroInput }
            .Where(x => !string.IsNullOrWhiteSpace(x))
            .Select(x => Normalizar(x!))
            .ToList();

        string? estadoCorreto = null;
        string? cidadeCorreta = null;
        string? bairroCorreto = null;

        // Tentar encontrar estado pela sigla ou nome
        foreach (var estado in _estados)
        {
            var nomeNorm = Normalizar(estado.Nome);
            var siglaNorm = Normalizar(estado.Sigla);

            if (todosNomes.Contains(nomeNorm) || todosNomes.Contains(siglaNorm))
            {
                estadoCorreto = estado.Sigla;

                // Tentar descobrir cidade a partir da lista
                foreach (var cidade in estado.Cidades)
                {
                    var cidadeNorm = Normalizar(cidade);
                    if (todosNomes.Contains(cidadeNorm))
                    {
                        cidadeCorreta = cidade;
                        break;
                    }
                }
                break; // Estado encontrado, sai do loop
            }
        }

        // Caso ainda não tenhamos cidade, buscar globalmente
        if (cidadeCorreta == null)
        {
            foreach (var estado in _estados)
            {
                foreach (var cidade in estado.Cidades)
                {
                    var cidadeNorm = Normalizar(cidade);
                    if (todosNomes.Contains(cidadeNorm))
                    {
                        cidadeCorreta = cidade;
                        estadoCorreto = estado.Sigla;
                        break;
                    }
                }
                if (cidadeCorreta != null) break;
            }
        }

        // O que sobrou, pode ser bairro
        var usados = new HashSet<string?>
        {
            Normalizar(estadoCorreto),
            Normalizar(cidadeCorreta)
        };

        bairroCorreto = todosNomes
            .FirstOrDefault(x => !usados.Contains(x));

        return new EnderecoCorrigido
        {
            Estado = estadoCorreto ?? "",
            Cidade = cidadeCorreta ?? "",
            Bairro = bairroCorreto ?? ""
        };
    }

    private static string Normalizar(string input)
    {
        return new string(input
            .ToLowerInvariant()
            .Normalize(NormalizationForm.FormD)
            .Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
            .ToArray())
            .Trim();
    }
}

public static class DataNascimentoExtensions
{
    private static readonly string[] DateFormats = new[]
    {
        "dd/MM/yyyy", "d/M/yyyy", "yyyy-MM-dd", "dd-MM-yyyy",
        "dd.MM.yyyy", "d.MM.yyyy", "dd MMM yyyy", "d MMM yyyy",
        "dd 'de' MMMM 'de' yyyy", "d 'de' MMMM 'de' yyyy"
    };

    private static readonly CultureInfo[] Cultures = new[]
    {
        CultureInfo.InvariantCulture,
        new CultureInfo("pt-BR"),
        new CultureInfo("en-US")
    };

    public static DateOnly? TryParseBirthDate(this string? input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return null;

        string cleaned = Regex.Replace(input, @"(?i)\b(nascid[oa]?|em|data de nascimento|nasc\.)\b", "", RegexOptions.CultureInvariant);
        cleaned = cleaned.Trim();

        foreach (var culture in Cultures)
        {
            foreach (var format in DateFormats)
            {
                if (DateTime.TryParseExact(cleaned, format, culture, DateTimeStyles.None, out var dt))
                    return DateOnly.FromDateTime(dt);
            }

            // fallback parse
            if (DateTime.TryParse(cleaned, culture, DateTimeStyles.AllowWhiteSpaces, out var dtParsed))
                return DateOnly.FromDateTime(dtParsed);
        }

        return null;
    }
}
