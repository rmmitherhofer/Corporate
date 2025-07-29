using System.Text.Json.Serialization;

namespace PanPan.WebApp.Models.Response.NewFolder;
public class ParserHttpResponse
{
    [JsonPropertyName("notifications")]
    public IEnumerable<NotificationResponse>? Notifications { get; set; }

    [JsonPropertyName("resume")]
    public ResumeHttpResponse Resume { get; set; }
}

public class ResumeHttpResponse
{
    [JsonPropertyName("registration")]
    public RegistrationHttpResponse? Registration { get; set; }

    [JsonPropertyName("address")]
    public AddressHttpResponse? Address { get; set; }

    [JsonPropertyName("languages")]
    public IEnumerable<LanguageHttpResponse>? Languages { get; set; }

    [JsonPropertyName("skills")]
    public SkillsHttpResponse? Skills { get; set; }

    [JsonPropertyName("experiences")]
    public IEnumerable<ExperienceHttpResponse>? Experiences { get; set; }

    [JsonPropertyName("formations")]
    public IEnumerable<FormationHttpResponse>? Formations { get; set; }

    [JsonPropertyName("courses")]
    public IEnumerable<FormationHttpResponse>? Courses { get; set; }
}

public class RegistrationHttpResponse
{
    [JsonPropertyName("nomeCompleto")]
    public string? FullName { get; set; }

    [JsonPropertyName("sexo")]
    public string? Gender { get; set; }

    [JsonPropertyName("email")]
    public string? Email { get; set; }

    [JsonPropertyName("telefone")]
    public string? PhoneNumber { get; set; }

    [JsonPropertyName("celular")]
    public string? MobileNumber { get; set; }

    [JsonPropertyName("linkedinUrl")]
    public string? LinkedInUrl { get; set; }

    [JsonPropertyName("githubUrl")]
    public string? GitHubUrl { get; set; }

    [JsonPropertyName("objetivo")]
    public string? Goals { get; set; }

    [JsonPropertyName("idade")]
    public string? Age { get; set; }

    [JsonPropertyName("dataNascimento")]
    public DateTime? BrithDate { get; set; }

    [JsonPropertyName("estadoCivil")]
    public string? MaterialStatus { get; set; }

    [JsonPropertyName("cnh")]
    public string? CNH { get; set; }
}

public class AddressHttpResponse
{
    [JsonPropertyName("logradouro")]
    public string? Street { get; set; }

    [JsonPropertyName("numero")]
    public string? Number { get; set; }

    [JsonPropertyName("complemento")]
    public string? Complement { get; set; }

    [JsonPropertyName("bairro")]
    public string? Bairro { get; set; }

    [JsonPropertyName("cidade")]
    public string? City { get; set; }

    [JsonPropertyName("estado")]
    public string? State { get; set; }
}

public class LanguageHttpResponse
{
    [JsonPropertyName("idioma")]
    public string? Language { get; set; }

    [JsonPropertyName("nivel")]
    public string? Level { get; set; }
}

public class SkillsHttpResponse
{
    [JsonPropertyName("softSkills")]
    public List<string>? SoftSkills { get; set; }

    [JsonPropertyName("hardSkills")]
    public List<string>? HardSkills { get; set; }
}

public class ExperienceHttpResponse
{
    [JsonPropertyName("empresa")]
    public string? Company { get; set; }

    [JsonPropertyName("cargo")]
    public string? Cargo { get; set; }

    [JsonPropertyName("dataInicio")]
    public string? StartDate { get; set; }

    [JsonPropertyName("dataFim")]
    public string? EndDate { get; set; }

    [JsonPropertyName("descricao")]
    public string? Description { get; set; }
}

public class FormationHttpResponse
{
    [JsonPropertyName("nomeInstituicao")]
    public string? InstitutionName { get; set; }

    [JsonPropertyName("descricaoCurso")]
    public string? Description { get; set; }

    [JsonPropertyName("dataInicio")]
    public string? StartDate { get; set; }

    [JsonPropertyName("dataFim")]
    public string? EndDate { get; set; }

    [JsonPropertyName("nivel")]
    public string? Level { get; set; }
}
