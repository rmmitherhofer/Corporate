using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum Curso
{
    [Description("Curso técnico")]
    Tecnico = 1,
    [Description("Curso profissionalizante")]
    Profissionalizante = 2
}
