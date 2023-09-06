using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum Curso
{
    [Description("Curso técnica")]
    Tecnico = 1,
    [Description("Curso profissionalizante")]
    Profissionalizante = 2
}
