using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Candidatos.Business.Artefatos;

public class ArtefatosRHF
{
    public FonteRHF Fonte { get; }
    public CorRhf Cor { get; }
    public ImagemRhf Imagem { get; }

    public ArtefatosRHF()
    {
        Fonte = new();
        Cor = new();
        Imagem = new();
    }
}
public sealed class FonteRHF
{
    public BaseFont GothamBookItalic { get; }
    public BaseFont GothamBook { get; }
    public BaseFont GothamLightItalic { get; }
    public BaseFont GothamBold { get; }
    public BaseFont GothamBoldItalic { get; }
    public BaseFont GothamLight { get; }
    public BaseFont GothamMedium { get; }
    public FonteRHF()
    {
        var baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

        GothamBookItalic = BaseFont.CreateFont($"{baseDirectory}/Assets/fonts/GothamBookItalic.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        GothamBook = BaseFont.CreateFont($"{baseDirectory}/Assets/fonts/GothamBook.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        GothamLightItalic = BaseFont.CreateFont($"{baseDirectory}/Assets/fonts/GothamLightItalic.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

        GothamBold = BaseFont.CreateFont($"{baseDirectory}/Assets/fonts/GothamBold.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        GothamBoldItalic = BaseFont.CreateFont($"{baseDirectory}/Assets/fonts/GothamBoldItalic.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        GothamLight = BaseFont.CreateFont($"{baseDirectory}/Assets/fonts/GothamLight.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
        GothamMedium = BaseFont.CreateFont($"{baseDirectory}/Assets/fonts/GothamMedium.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
    }
}
public sealed class CorRhf
{
    public BaseColor Laranja { get; }
    public BaseColor Azul { get; }
    public BaseColor LaranjaClaro { get; }
    public BaseColor AzulClaro { get; }
    public BaseColor Branco { get; }
    public BaseColor Preto { get; }
    public BaseColor Cinza { get; }
    public BaseColor CinzaEscuro { get; }
    public CorRhf()
    {
        Laranja = new(217, 99, 19, 1);
        Azul = new(2, 74, 110, 1);
        LaranjaClaro = new(224, 139, 65, 1);
        AzulClaro = new(6, 115, 158, 1);
        Branco = new(255, 255, 255, 1);
        Preto = new(1, 1, 1, 1);
        Cinza = new(132, 132, 132, 1);
        CinzaEscuro = new(51, 51, 51, 1);
    }
}
public sealed class ImagemRhf
{
    private static string Diretorio = AppDomain.CurrentDomain.BaseDirectory;
    public static string MarcaDAguaPNG = $"{Diretorio}/Assets/images/marca-agua-rhf.png";
    public static string LogoAzulPNG = $"{Diretorio}/Assets/images/logo-blue-rhf.png";
    public static string LogoLaranjaFundoAzulPNG = $"{Diretorio}/Assets/images/logo-orange-blue-rhf.png";
    public static string LogoLaranjaFundoBrancoPNG = $"{Diretorio}/Assets/images/logo-orange-white-rhf.png";
}