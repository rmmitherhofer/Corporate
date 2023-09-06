using Candidatos.Business.Artefatos;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Candidatos.Business.Factories;

public class TemplatePdfFactory : PdfPageEventHelper
{
    private readonly ArtefatosRHF _rhf = new();
    private PdfContentByte _contentByte;
    private PdfTemplate _headerTemplate, _footerTemplate;
    public bool UsarTemplateEmCadaPagina { get; private set; }
    public TemplatePdfFactory()
    {
        UsarTemplateEmCadaPagina = false;
    }

    public override void OnOpenDocument(PdfWriter writer, Document document)
    {
        _contentByte = writer.DirectContent;
        _headerTemplate = _contentByte.CreateTemplate(100, 100);
        _footerTemplate = _contentByte.CreateTemplate(50, 50);
    }
    public override void OnStartPage(PdfWriter writer, Document document)
    {
        base.OnStartPage(writer, document);
    }
    public override void OnEndPage(PdfWriter writer, Document document)
    {
        base.OnEndPage(writer, document);

        if (UsarTemplateEmCadaPagina)
        {
            CriarHeaderEmCadaPagina(writer, document);
            CriarFooterEmCadaPagina(writer, document);
        }        
    }
    public override void OnCloseDocument(PdfWriter writer, Document document)
    {
        base.OnCloseDocument(writer, document);

        _headerTemplate.BeginText();
        _headerTemplate.SetFontAndSize(_rhf.Fonte.GothamBook, 8);
        _headerTemplate.SetTextMatrix(0, 0);
        _headerTemplate.ShowText((writer.PageNumber - 1).ToString());
        _headerTemplate.EndText();

        _footerTemplate.BeginText();
        _footerTemplate.SetFontAndSize(_rhf.Fonte.GothamBook, 8);
        _footerTemplate.SetTextMatrix(0, 0);
        _footerTemplate.ShowText((writer.PageNumber - 1).ToString());
        _footerTemplate.EndText();
    }
    public void CriarLinhaDivisoraDeConteudo(Document document, float x, float y)
    {
        _contentByte.SetColorStroke(new BaseColor(240, 241, 245, 1));
        _contentByte.MoveTo(x, document.PageSize.Height - y);
        _contentByte.LineTo(document.PageSize.Width - x, document.PageSize.Height - y);
        _contentByte.Stroke();
    }

    #region HEADER
    private void CriarHeaderEmCadaPagina(PdfWriter writer, Document document)
    {
        CriarHeader(document).WriteSelectedRows(0, -1, 40, document.PageSize.GetTop(30), writer.DirectContent);
    }
    public void CriarHeaderPaginaInicial(Document document)
    {
        if (!UsarTemplateEmCadaPagina)
            document.Add(CriarHeader(document));
    }
    private PdfPTable CriarHeader(Document document) => Header.Criar(document);
    #endregion

    #region FOOTER
    private void CriarFooterEmCadaPagina(PdfWriter writer, Document document)
    {
        CriarFooter(document).WriteSelectedRows(0, -1, 0, document.PageSize.GetBottom(80), writer.DirectContent);
    }
    public void CriarFooterUltimaPagina(PdfWriter writer, Document document)
    {
        if (!UsarTemplateEmCadaPagina)
        {
            CriarFooter(document).WriteSelectedRows(0, -1, 0, document.PageSize.GetBottom(80), writer.DirectContent);
        }
    }
    private PdfPTable CriarFooter(Document document)
    {
        return Footer.Criar(document);
    }
    private void InserirNumeroDePaginas(PdfWriter writer, Document document, bool paginaTop = false, bool paginaFooter = false)
    {
        string text = "página " + writer.PageNumber + " de ";
        float len = _rhf.Fonte.GothamBook.GetWidthPoint(text, 8);
        const float marginRight = 90;
        const float marginVertical = 25;

        if (paginaTop)
        {
            _contentByte.BeginText();
            _contentByte.SetFontAndSize(_rhf.Fonte.GothamBook, 8);
            _contentByte.SetTextMatrix(document.PageSize.GetRight(marginRight), document.PageSize.GetTop(marginVertical));
            _contentByte.ShowText(text);
            _contentByte.EndText();
            _contentByte.AddTemplate(_headerTemplate, document.PageSize.GetRight(marginRight) + len, document.PageSize.GetTop(marginVertical));
        }

        if (paginaFooter)
        {
            _contentByte.BeginText();
            _contentByte.SetFontAndSize(_rhf.Fonte.GothamBook, 8);
            _contentByte.SetTextMatrix(document.PageSize.GetRight(marginRight), document.PageSize.GetBottom(marginVertical));
            _contentByte.ShowText(text);
            _contentByte.EndText();
            _contentByte.AddTemplate(_headerTemplate, document.PageSize.GetRight(marginRight) + len, document.PageSize.GetBottom(marginVertical));
        }
    }
    #endregion

    private static Image ObterImagemMarcaDAgua()
    {
        const float alturaImg = 625f;
        const float larguraImg = 625f;

        Image image = Image.GetInstance($"{AppDomain.CurrentDomain.BaseDirectory}/Assets/images/marca-agua-rhf.png");
        image.ScaleToFit(larguraImg, alturaImg);
        image.SetAbsolutePosition(80, 80);

        return image;
    }

    public byte[] AdicionarMarcaDAguaImagem(byte[] file)
    {
        using MemoryStream stream = new();

        var pdfReader = new PdfReader(file);
        var pdfStamper = new PdfStamper(pdfReader, stream);

        for (var i = 0; i < pdfReader.NumberOfPages; i++)
        {
            var content = pdfStamper.GetUnderContent(i + 1);
            content.AddImage(ObterImagemMarcaDAgua());
        }
        pdfStamper.Close();
        file = stream.ToArray();
        return file;
    }
}
