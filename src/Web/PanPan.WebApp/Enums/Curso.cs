using System.ComponentModel;

namespace PanPan.WebApp.Enums;

public enum Curso
{
    [Description("Curso complementar")]
    Complementar = 0,
    [Description("Curso técnico")]
    Tecnico = 1,
    [Description("Curso profissionalizante")]
    Profissionalizante = 2,
}
