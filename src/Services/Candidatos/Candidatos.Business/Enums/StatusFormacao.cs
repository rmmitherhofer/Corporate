using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum StatusFormacao
{
    [Description("Cursando")]
    Cursando = 1,
    [Description("Concluído")]
    Concluido = 3,
    [Description("Incompleto")]
    Incompleto = 4
}