using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum PeriodoFormacao
{
    [Description("Matutino")]
    Manha = 1,
    [Description("Vespertino")]
    Tarde = 2,
    [Description("Noturno")]
    Noite = 3,
    [Description("Integral")]
    Integral = 4
}
