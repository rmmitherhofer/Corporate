using System.ComponentModel;

namespace PanPan.WebApp.Enums;

public enum Ensino
{
    [Description("Ensino fundamental")]
    Fundamental = 1,
    [Description("Ensino médio")]
    Medio = 2,
    [Description("Ensino médio-técnico")]
    MedioTecnico =3,
    [Description("Ensino técnico")]
    Tecnico = 4,
    [Description("Ensino superior - Licenciatura")]
    SuperiorLicenciatura = 5,
    [Description("Ensino superior - Bacharelado")]
    SuperiorBacharelado = 6,
    [Description("Ensino superior - Tecnólogo")]
    SuperiorTecnologo = 7,
    [Description("Pós-graduação")]
    PosGraduacao = 8,
    [Description("Mestrado")]
    Mestrado =  9,
    [Description("Doutorado")]
    Doutorado = 10
}
