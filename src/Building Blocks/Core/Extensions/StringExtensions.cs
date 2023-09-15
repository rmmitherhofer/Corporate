using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Extensions;

public static class StringExtensions
{
    public static string MascaraCpfCnpj(this string cpfCnpj)
    {
        cpfCnpj = cpfCnpj.RemoverMascaraCpfCnpj();

        if (cpfCnpj.Length <= 11)
        {
            cpfCnpj = cpfCnpj.PadLeft(11, '0');
            cpfCnpj = cpfCnpj.Insert(9, "-");
            cpfCnpj = cpfCnpj.Insert(6, ".");
            return cpfCnpj.Insert(3, ".");
        }
        else
        {
            cpfCnpj = cpfCnpj.PadLeft(14, '0');
            cpfCnpj = cpfCnpj.Insert(12, "-");
            cpfCnpj = cpfCnpj.Insert(8, "/");
            cpfCnpj = cpfCnpj.Insert(5, ".");
            return cpfCnpj.Insert(2, ".");
        }
    }

    public static string RemoverMascaraCpfCnpj(this string cpfCnpj)
    {
        return cpfCnpj.Replace(".", "").Replace("-", "").Replace("/", "").Trim();
    }

    public static string MascaraInscricaoCpfCnpj(this string inscricao)
    {
        if (inscricao.Length == 8)
            return string.Format(@"{0:00\.000\.000}", double.Parse(inscricao));

        return string.Format(@"{0:000\.000\.000}", double.Parse(inscricao));
    }

    public static string MascaraTel(this string tel)
    {
        tel = tel.Trim();
        tel = tel.Replace(" ", "").Replace("(", "").Replace(")", "");
        if (tel.Length == 11)
        {
            tel = tel.Insert(7, "-");
            tel = tel.Insert(2, ") ");
            tel = tel.Insert(0, "(");
        }
        else if (tel.Length == 10)
        {
            tel = tel.Insert(6, "-");
            tel = tel.Insert(2, ") ");
            tel = tel.Insert(0, "(");
        }
        else
        {
            tel = string.Empty;
        }
        return tel;
    }

    public static string MascaraCep(this string cep)
    {
        cep = cep.Trim();
        cep = cep.Replace(" ", "").Replace("-", "").Replace(".", "");

        if (cep.Length == 8)
            return cep.Insert(5, "-");

        return string.Empty;
    }

    public static string ToTitleCase(this string texto)
    {
        var textInfo = new CultureInfo("pt-Br", false).TextInfo;
        return textInfo.ToTitleCase(texto.ToLower());
    }

    public static string IsNull(this string texto)
    {
        return string.IsNullOrEmpty(texto) ? string.Empty : texto;
    }

    public static string ReplaceAccents(this string texto)
    {
        return new string(texto.Normalize(NormalizationForm.FormD)
                            .Where(ch => char.GetUnicodeCategory(ch) != UnicodeCategory.NonSpacingMark)
                            .ToArray());
    }

    public static string RemoveSpaces(this string texto)
    {
        return texto.Replace(" ", "");
    }
}