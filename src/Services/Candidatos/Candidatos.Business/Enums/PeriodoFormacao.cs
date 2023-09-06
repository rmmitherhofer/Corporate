using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum PeriodoFormacao
{
    [Description("Manhã")]
    Manha = 1,
    [Description("Tarde")]
    Tarde = 2,
    [Description("Noturno ")]
    Noite = 3,
    [Description("Integral")]
    Integral = 4
}
