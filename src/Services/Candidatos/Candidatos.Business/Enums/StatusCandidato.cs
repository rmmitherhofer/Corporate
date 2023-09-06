using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum StatusCandidato
{
    [Description("Triagem")]
    Triagem = 1,
    [Description("Em Processo")]
    EmProcesso = 2,
    [Description("Banco de talentos")]
    BancoTalentos = 3,
    [Description("Black list")]
    BlackList = 4
}
public enum StatusRecrutamento
{
    [Description("Em Recrutamento")]
    EntrevistaInterna = 3,

    [Description("Sem interesse no proposta")]
    SemInteresse = 3,

    [Description("Possui interesse na proposta")]
    PossuiInteresse = 3,

    [Description("Nao selecionado")]
    NaoSelecionado = 3,

    [Description("Aguardando entrevista interna")]
    AguardandoEntrevistaInterna = 3,

    [Description("Aprovado na entrevista interna")]
    AprovadoEntrevistaInterna = 3,

    [Description("Reprovado na entrevista interna")]
    ReprovadoEntrevistaInterna = 4,

    [Description("Aguardando gerar parecer")]
    AguardandoParecer = 3,

    [Description("Aguardando avaliacao do parecer")]
    AguardandoAvaliacaoParecer = 3,

    [Description("Parecer aprovado")]
    ParecerAprovado = 3,

    [Description("Parecer reprovado")]
    ParecerReprovado = 3,

    [Description("Aguardando entrevista com cliente")]
    AguardandoEntrevistaCliente = 3,

    [Description("Aprovado na entrevista com cliente")]
    AprovadoEntrevistaCliente = 3,

    [Description("Reprovado na entrevista com cliente")]
    ReprovadoEntrevistaCliente = 4,

    [Description("Aguardando contratacao")]
    AguardandoContratacao = 4,

    [Description("Contratado")]
    Contratado = 4,

    [Description("Aguardando feedback")]
    AguardandoFeedback = 4
}