using Candidatos.Business.Artefatos;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Candidatos.Business.Factories;

public static class Footer
{
    private readonly static ArtefatosRHF _rhf = new(); 
    private static readonly bool _incluirBorda = false;
    const int _fullBorder = Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER | Rectangle.BOTTOM_BORDER;

    const float TamanhoFonte = 7f;

    public static PdfPTable Criar(Document document)
    {
        var header = new PdfPTable(1)
        {
            TotalWidth = document.PageSize.Width,
            LockedWidth = true

        };

        CriarCelula(ref header, "Unidade Boituva - SP");
        CriarCelula(ref header, "e-mail: pamela.mither@rhf.com.br • www.rhf.com.br/pamelamither");
        CriarCelula(ref header, "Fone: 55 15 99837-1212");

        return header;
    }
    private static void CriarCelula(ref PdfPTable table, string valor)
    {
        var celulaTitulo = new CelulaPdf(new Phrase(valor, new Font(_rhf.Fonte.GothamLight, TamanhoFonte, Font.NORMAL, _rhf.Cor.CinzaEscuro)))
            .DefinirAlinhamento(Element.ALIGN_CENTER, Element.ALIGN_CENTER)
            .DefinirBorda(_fullBorder, 0.2f, _rhf.Cor.Preto, _rhf.Cor.Preto, _incluirBorda)
            .DefinirPadding(4f, 0, 4f, 0);

        CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaTitulo);
    }
}