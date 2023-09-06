using System.ComponentModel;

namespace PanPan.WebApp.Enums;

public enum StatusCivil
{
    [Description("Indefinido(a)")]
    Indefinido = 0,
    [Description("Solteiro(a)")]
    Solteiro = 1,
    [Description("Casado(a)")]
    Casado = 2,
    [Description("Separado(a)")]
    Separado = 3,
    [Description("Divorciado(a)")]
    Divorciado = 4,
    [Description("Viúvo(a)")]
    Viuvo = 5
}