using System.ComponentModel;

namespace PanPan.WebApp.Enums;

public enum RecrutadoPor
{
    [Description("Catho")]
    Catho = 1,
    [Description("Vagas.com")]
    VagasPontoCom = 2,
    [Description("InfoJobs")]
    InfoJobs = 3,
    [Description("Entrou em contato - e-mail")]
    Email = 4,
    [Description("BNE")]
    BancoNacionalDeCurriculos = 5,
    [Description("Burh")]
    Burh = 6,
    [Description("Entrou em contato - WhatsApp")]
    WhatsApp = 7,
    [Description("Empregos.com")]
    EmpregosPontoCom = 8,
    [Description("Linkedin")]
    Linkedin = 9,
}
