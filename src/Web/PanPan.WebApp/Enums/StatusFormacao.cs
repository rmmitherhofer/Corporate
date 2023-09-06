using System.ComponentModel;

namespace PanPan.WebApp.Enums;

public enum StatusFormacao
{
    [Description("Cursando")]
    Cursando = 1,
    [Description("Concluído")]
    Concluido = 3,
    [Description("Incompleto")]
    Incompleto = 4
}