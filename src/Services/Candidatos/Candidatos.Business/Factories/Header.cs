using Candidatos.Business.Artefatos;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace Candidatos.Business.Factories;

public static class Header
{
    private static readonly ArtefatosRHF _rhf = new();
    const int _fullBorder = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;
    private static readonly bool _incluirBorda = false;

    public static PdfPTable Criar(Document document)
    {
        var header = new PdfPTable(2)
        {            
            TotalWidth = document.PageSize.Width,
            WidthPercentage = 100,
            LockedWidth = false
        };

        float[] largura = { 76f, 130f };
        header.SetWidths(largura);

        CriarCelulaLogo(ref header);

        CriarCelulaTitulo(ref header);

        header.SpacingAfter = 12f;

        return header;
    }

    private static void CriarCelulaLogo(ref PdfPTable table)
    {
        var celulaLogo = new CelulaPdf(CriarLogo(), _rhf.Cor.Azul)
            .DefinirAlinhamento(Element.ALIGN_CENTER, Element.ALIGN_TOP)
            .DefinirBorda(_fullBorder, 0.2f, _rhf.Cor.Preto, _rhf.Cor.Preto, _incluirBorda)
            .DefinirPadding(0, 0, 10f, 0);

        CelulaPdf.IncluirCelulaDeImagemNaTabela(ref table, celulaLogo);
    }
    private static void CriarCelulaTitulo(ref PdfPTable table)
    {
        const float tamanhoFonte = 20f;
        const int alturaLinha = 30;

        var celulaTitulo = new CelulaPdf(new Phrase("PERFIL DO CANDIDATO", new Font(_rhf.Fonte.GothamBold, tamanhoFonte, Font.NORMAL, _rhf.Cor.Branco)), _rhf.Cor.Azul, alturaLinha)
            .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_CENTER)
            .DefinirBorda(_fullBorder, 0.2f, _rhf.Cor.Preto, _rhf.Cor.Preto, _incluirBorda)
            .DefinirPadding(40f, 0, 0, 0);

        CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaTitulo);
    }
    private static Image CriarLogo()
    {
        const float alturaImg = 165f;
        const float larguraImg = 165f;

        Image logo = Image.GetInstance($"{AppDomain.CurrentDomain.BaseDirectory}/Assets/images/logo-orange-white-rhf.png");
        logo.ScaleToFit(larguraImg, alturaImg);

        return logo;
    }
}
