using System.ComponentModel;

namespace PanPan.WebApp.Enums;

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