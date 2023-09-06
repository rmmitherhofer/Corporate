using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum StatusCivil
{
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
