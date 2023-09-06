using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum RecrutadoPor
{
    [Description("Catho")]
    Catho = 1,
    [Description("Vagas.com")]
    VagasPontoCom = 2,
    [Description("InfoJobs")]
    InfoJobs = 3,
    [Description("e-mail")]
    Email = 4,
    [Description("BNC")]
    BancoNacionalDeCurriculos = 5,
    [Description("Burh")]
    Burh = 6
}
