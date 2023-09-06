using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Candidatos.Business.Factories;

public class CelulaPdf
{
    public Phrase Frase { get; }
    public Image Image { get; }
    public float PaddingTop { get; private set; }
    public float PaddingBottom { get; private set; }
    public float PaddingLeft { get; private set; }
    public float PaddingRight { get; private set; }
    public int Border { get; private set; }
    public float BorderWidth { get; private set; }
    public BaseColor BorderBaseColorTpBot { get; private set; }
    public BaseColor BorderBaseColorLftRgt { get; private set; }
    public BaseColor BackgroundBaseColor { get; private set; }
    public int Colspan { get; private set; }
    public int HorizontalAligment { get; private set; }
    public int VerticalAligment { get; private set; }
    public int FixedHeight { get; private set; }
    public int FixedWidth { get; private set; }

    /// <summary>
    /// Instancia uma nova celula com uma frase
    /// </summary>
    /// <param name="frase"></param>
    /// <param name="backgroundBaseColor"></param>
    /// <param name="fixedHeight">Altura da celula. 0 por padrao, ira pegar o tamanho do texto.</param>
    /// <param name="colspan"></param>
    public CelulaPdf(Phrase frase, BaseColor? backgroundBaseColor = null, int fixedHeight = 0, int colspan = 1)
    {
        Frase = frase;
        BackgroundBaseColor = backgroundBaseColor;
        Colspan = colspan;
        FixedHeight = fixedHeight;
    }

    public CelulaPdf(Image image, BaseColor? backgroundBaseColor = null, int fixedHeight = 0, int colspan = 1)
    {
        Image = image;
        BackgroundBaseColor = backgroundBaseColor;
        Colspan = colspan;
        FixedHeight = fixedHeight;
    }

    public static void IncluirCelulaDeImagemNaTabela(ref PdfPTable table, CelulaPdf celula)
    {
        try
        {
            table.AddCell(new PdfPCell(celula.Image)
            {
                PaddingTop = celula.PaddingTop,
                PaddingRight = celula.PaddingRight,
                PaddingBottom = celula.PaddingBottom,
                PaddingLeft = celula.PaddingLeft,
                Border = celula.Border,
                BorderWidth = celula.BorderWidth,
                BorderColorTop = celula.BorderBaseColorTpBot,
                BorderColorBottom = celula.BorderBaseColorTpBot,
                BorderColorLeft = celula.BorderBaseColorLftRgt,
                BorderColorRight = celula.BorderBaseColorLftRgt,
                Colspan = celula.Colspan,
                BackgroundColor = celula.BackgroundBaseColor,
                HorizontalAlignment = celula.HorizontalAligment,
                VerticalAlignment = celula.VerticalAligment,
                FixedHeight = celula.FixedHeight,
                UseVariableBorders = true
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao incluir celula: {ex.Message}", ex);
        }
    }

    public static void IncluirCelulaDeFraseNaTabela(ref PdfPTable table, CelulaPdf celula)
    {
        try
        {
            table.AddCell(new PdfPCell(celula.Frase)
            {
                PaddingTop = celula.PaddingTop,
                PaddingRight = celula.PaddingRight,
                PaddingBottom = celula.PaddingBottom,
                PaddingLeft = celula.PaddingLeft,
                Border = celula.Border,
                BorderWidth = celula.BorderWidth,
                BorderColorTop = celula.BorderBaseColorTpBot,
                BorderColorBottom = celula.BorderBaseColorTpBot,
                BorderColorLeft = celula.BorderBaseColorLftRgt,
                BorderColorRight = celula.BorderBaseColorLftRgt,
                Colspan = celula.Colspan,
                BackgroundColor = celula.BackgroundBaseColor,
                HorizontalAlignment = celula.HorizontalAligment,
                VerticalAlignment = celula.VerticalAligment,
                FixedHeight = celula.FixedHeight,
                UseVariableBorders = true
            });
        }
        catch (Exception ex)
        {
            throw new Exception($"Erro ao incluir celula: {ex.Message}", ex);
        }
    }

    public CelulaPdf DefinirPadding(float paddingTop, float paddingRight, float paddingBottom, float paddingLeft)
    {
        PaddingBottom = paddingBottom;
        PaddingLeft = paddingLeft;
        PaddingTop = paddingTop;
        PaddingRight = paddingRight;

        return this;
    }

    public CelulaPdf DefinirBorda(int border, float borderWidth, BaseColor borderBaseColorTpBot, BaseColor borderBaseColorLftRgt, bool incluirBorda = true)
    {
        if (!incluirBorda) return this;

        Border = border;
        BorderWidth = borderWidth;
        BorderBaseColorTpBot = borderBaseColorTpBot;
        BorderBaseColorLftRgt = borderBaseColorLftRgt;
        return this;
    }

    /// <summary>
    /// Define o alinhamento vertical e horizontal da celula
    /// </summary>
    /// <param name="alinhamentoHorizontal">Esquerda = 0 / Centro = 1 / Direita = 2</param>
    /// <param name="alinhamentoVertical">Cima = 4 / Meio = 5 / Baixo = 6</param>
    /// <returns></returns>
    public CelulaPdf DefinirAlinhamento(int alinhamentoHorizontal, int alinhamentoVertical)
    {
        HorizontalAligment = alinhamentoHorizontal;
        VerticalAligment = alinhamentoVertical;

        return this;
    }
}
