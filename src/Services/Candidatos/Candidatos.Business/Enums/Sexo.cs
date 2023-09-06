using System.ComponentModel;

namespace Candidatos.Business.Enums;

public enum Sexo
{
    [Description("Masculino")]
    Masculino = 1,
    [Description("Feminino")]
    Feminino = 2,
}

public enum Domicilio
{
    [Description("Alugado(a)")]
    Alugado = 1,
    [Description("Próprio(a)")]
    Próprio = 2,
}

public enum TipoDomicilio
{
    [Description("Casa")]
    Casa = 1,
    [Description("Apartamento")]
    Apartamento = 2,
    [Description("Chácara")]
    Chacara = 3,
    [Description("Sítio")]
    Sitio = 4,
    [Description("Fazenda")]
    Fazenda = 5,
}