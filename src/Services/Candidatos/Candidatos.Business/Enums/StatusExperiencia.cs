using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum StatusExperiencia
{
    [Description("Atual")]
    Atual = 1,
    [Description("Desligado")]
    Desligado = 2,
    [Description("Promoção")]
    Proomocao = 3
}
