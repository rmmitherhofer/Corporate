using System.ComponentModel;

namespace PanPan.WebApp.Enums;

public enum RegimeContratacao
{
    [Description("CLT")]
    CLT = 1,
    [Description("PJ")]
    PJ = 2,
    [Description("Estagio")]
    Estagio = 3,
    [Description("Temporario")]
    Temporario = 4,
    [Description("Terceirizado")]
    Terceirizado = 5,
    [Description("Autonomo")]
    Autonomo = 6,
    [Description("Sem Registro")]
    SemRegistro = 7
}