using Candidatos.Business.Artefatos;
using Candidatos.Business.Enums;
using Candidatos.Business.Models;
using Extensions;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Notifications.Interfaces;
using Notifications.Messages;
using System.IO.Pipes;

namespace Candidatos.Business.Factories;

public interface IParecerPdfFactory
{
    byte[] Gerar(Candidato candidato);
}
public class ParecerPdfFactory : IParecerPdfFactory
{
    private readonly INotificationHandler _notification;
    private readonly TemplatePdfFactory _template;
    private readonly ArtefatosRHF _rhf;
    private readonly bool _incluirBorda = false;
    const float _margemTabela = 90f;
    const float _tamanhoFonteTitulo = 19f;
    const float _tamanhoFonteSubTitulo = 10f;
    const float _tamanhoFonteText = 8.5f;


    public ParecerPdfFactory(INotificationHandler notification)
    {
        _rhf = new();
        _template = new();
        _notification = notification;
    }

    public byte[] Gerar(Candidato candidato)
    {
        using MemoryStream stream = new();

        Document document = new(PageSize.A4, 20f, 20f, 20f, 20f);
        var writer = PdfWriter.GetInstance(document, stream);

        writer.PageEvent = _template;

        document.Open();

        _template.CriarHeaderPaginaInicial(document);

        Criar(document, candidato);

        _template.CriarFooterUltimaPagina(writer, document);

        document.Close();

        return _template.AdicionarMarcaDAguaImagem(stream.ToArray());
    }
    private void Criar(Document document, Candidato candidato)
    {
        TabelaDadosPessoais(document, candidato);
        TabelaEscolaridade(document, candidato.Formacoes);
        TabelaCursosComplementarees(document, candidato.Formacoes);
        TabelaDadosProfissionais(document, candidato.Experiencias);
        Conclusao(document, candidato);
    }

    private void TabelaDadosPessoais(Document document, Candidato candidato)
    {
        bool ehMasculino = candidato.Sexo == Sexo.Masculino;
        const int maxColunas = 4;

        PdfPTable table = new(maxColunas)
        {
            TotalWidth = document.PageSize.Width - _margemTabela,
            LockedWidth = true,
            SpacingAfter = 2f
        };

        #region Linha 1
        string[] valoresLinhaUm = { candidato.Apelido };
        int[] colspanLinhaUm = { maxColunas };
        TituloTabelaSemBorda(ref table, valoresLinhaUm, colspanLinhaUm);
        #endregion

        #region Linha 2
        string[] titulosLinhaDois = { "Idade", "Estado civil", "Possui filhos", "Pretensão salarial" };
        string[] valoresLinhaDois = { $"{candidato.Idade} anos", $"{(ehMasculino ? candidato?.StatusCivil?.Descricao()?.Replace("o(a)", "o") : candidato?.StatusCivil?.Descricao()?.Replace("o(a)", "a"))}", candidato.QuantidadeFilhos > 0 ? "Sim" : "Não", candidato.PretensaoSalarial == "0" ? "Aceita o proposto" : $"R$ {candidato.PretensaoSalarial}" };
        int[] colspanLinhaDois = { 1, 1, 1, 1 };
        LinhaComTopBorder(ref table, titulosLinhaDois, valoresLinhaDois, colspanLinhaDois, _incluirBorda);
        #endregion

        if (!string.IsNullOrEmpty(candidato.Endereco.Logradouro))
        {
            #region Linha 3
            string[] titulosLinhaTres = { "Endereço", "Complemento" };
            string[] valoresLinhaTres = { $"{candidato.Endereco.Logradouro}, {candidato.Endereco.Numero}", candidato.Endereco.Complemento };
            int[] colspanLinhaTres = { 3, 1 };

            if (string.IsNullOrEmpty(candidato.Endereco.Complemento))
            {
                titulosLinhaTres = new string[] { "Endereço" };
                valoresLinhaTres = new string[] { $"{candidato.Endereco.Logradouro}, {candidato.Endereco.Numero}" };
                colspanLinhaTres = new int[] { maxColunas };
            }

            LinhaComTopBorder(ref table, titulosLinhaTres, valoresLinhaTres, colspanLinhaTres, _incluirBorda);
            #endregion
        }

        #region Linha 4
        string[] titulosLinhaQuatro = { "Cidade", "UF", "CEP" };
        string[] valoresLinhaQuatro = { candidato.Endereco.Cidade, candidato.Endereco.Estado, candidato.Endereco.Cep };
        int[] colspanLinhQuatro = { 2, 1, 1 };

        if (string.IsNullOrEmpty(candidato.Endereco.Cep))
        {
            titulosLinhaQuatro = new string[] { "Cidade", "UF" };
            valoresLinhaQuatro = new string[] { candidato.Endereco.Cidade, candidato.Endereco.Estado };
            colspanLinhQuatro = new int[] { 2, 2 };
        }

        LinhaComBottomBorder(ref table, titulosLinhaQuatro, valoresLinhaQuatro, colspanLinhQuatro, _incluirBorda);
        #endregion

        document.Add(table);
    }

    private void TabelaEscolaridade(Document document, IEnumerable<Formacao> formacoes)
    {
        const int maxColunas = 1;

        var ensinos = formacoes.Where(f => f.Tipo == TipoFormacao.Ensino)
            .OrderByDescending(f => f.DataInicio)
            .ThenByDescending(f => f.DataFim)
            .ThenByDescending(f => f.Ensino);

        if (!ensinos.Any()) return;

        PdfPTable table = new(maxColunas)
        {
            TotalWidth = document.PageSize.Width - 90f,
            LockedWidth = true,
            SpacingAfter = 4f,
            SpacingBefore = 4f
        };

        #region Titulo
        string[] valoresTitulo = { "Escolaridade" };
        int[] colspanTitulo = { maxColunas };
        SubTituloTabelaSemBorda(ref table, valoresTitulo, colspanTitulo);
        #endregion

        document.Add(table);

        foreach (var ensino in ensinos)
        {
            PdfPTable tableContent = new(maxColunas)
            {
                TotalWidth = document.PageSize.Width - 90f,
                LockedWidth = true,
                SpacingAfter = 2f
            };

            #region Linha 1
            string[] valoresLinhaUm = { $"{ensino.Ensino.Value.Descricao()}{(ensino.Ensino > Ensino.Medio ? $" {ensino.Descricao}" : string.Empty)} - {ensino.Status.Descricao()}{(ensino.Status == StatusFormacao.Cursando ? $" - previsão de conclusão em {ensino.DataFim?.ToString("MM/yyyy")} - período {ensino.Periodo.Descricao()?.ToLower()}" : string.Empty)}" };
            int[] colspanLinhaUm = { maxColunas };
            LinhaTabelaSemBorda(ref tableContent, valoresLinhaUm, colspanLinhaUm);
            #endregion

            document.Add(tableContent);
        }
    }

    private void TabelaCursosComplementarees(Document document, IEnumerable<Formacao> formacoes)
    {
        const int maxColunas = 1;
        var cursos = formacoes.Where(f => f.Tipo == TipoFormacao.Curso)
            .OrderByDescending(f => f.DataInicio)
            .ThenByDescending(f => f.DataFim);

        if (!cursos.Any()) return;

        PdfPTable table = new(maxColunas)
        {
            TotalWidth = document.PageSize.Width - 90f,
            LockedWidth = true,
            SpacingAfter = 2f
        };

        #region Titulo
        string[] valoresTitulo = { "Cursos complementares" };
        int[] colspanTitulo = { maxColunas };
        SubTituloTabelaSemBorda(ref table, valoresTitulo, colspanTitulo);
        #endregion

        document.Add(table);

        foreach (var curso in cursos)
        {
            PdfPTable tableContent = new(maxColunas)
            {
                TotalWidth = document.PageSize.Width - 90f,
                LockedWidth = true,
                SpacingAfter = 2f
            };

            #region Linha 1
            string[] valoresLinhaUm = { $"{curso.Curso?.Descricao()} em {curso.Descricao} - {curso.NomeInstituicao} - {curso.Status.Descricao()}" };
            int[] colspanLinhaUm = { maxColunas };
            LinhaTabelaSemBorda(ref tableContent, valoresLinhaUm, colspanLinhaUm);
            #endregion

            document.Add(tableContent);
        }
    }

    private void TabelaDadosProfissionais(Document document, IEnumerable<Experiencia> experiencias)
    {
        const int maxColunas = 3;

        PdfPTable table = new(maxColunas)
        {
            TotalWidth = document.PageSize.Width - 90f,
            LockedWidth = true,
            SpacingAfter = 5f,
            SpacingBefore = 5f,
        };

        if (!experiencias.Any()) return;

        #region Titulo
        string[] valoresTitulo = { "Histórico profissional" };
        int[] colspanTitulo = { maxColunas };
        SubTituloTabelaSemBorda(ref table, valoresTitulo, colspanTitulo);
        #endregion

        document.Add(table);

        bool usarBorda = false;

        foreach (var experiencia in experiencias.OrderByDescending(e => e.DataInicio).ThenByDescending(e => e.DataFim))
        {
            PdfPTable tableContent = new(maxColunas)
            {
                TotalWidth = document.PageSize.Width - 90f,
                LockedWidth = true,
                SpacingAfter = 2f
            };

            float[] largura = { 130f, 90f, 50f };
            tableContent.SetWidths(largura);

            #region Linha 1
            string[] titulosLinhaUm = { "Empresa", "Função", "Período" };
            string[] valoresLinhaUm = { experiencia.Empresa, experiencia.Cargo, $"{experiencia.DataInicio:MM/yyyy} - {(experiencia.DataFim.HasValue ? experiencia.DataFim.Value.ToString("MM/yyyy") : experiencia.Status.Descricao())}" };
            int[] colspanLinhaUm = { 1, 1, 1 };
            PrimeiraLinhaComTopBorder(ref tableContent, titulosLinhaUm, valoresLinhaUm, colspanLinhaUm, _incluirBorda);
            #endregion

            #region Linha 3
            string[] titulosLinhaTres = { "Atribuições" };
            string[] valoresLinhaTres = { experiencia.Atividade };
            int[] colspanLinhaTres = { maxColunas };
            LinhaComTopBorder(ref tableContent, titulosLinhaTres, valoresLinhaTres, colspanLinhaTres, _incluirBorda);

            #endregion

            #region Linha 4
            string[] titulosLinhaQuatro = { "Motivo de saída" };
            string[] valoresLinhaQuatro = { experiencia.MotivoDesligamento };
            int[] colspanLinhaQuatro = { maxColunas };
            UltimaLinhaDivisaoTabela(ref tableContent, titulosLinhaQuatro, valoresLinhaQuatro, colspanLinhaQuatro);
            #endregion

            document.Add(tableContent);
        }
    }

    private void Conclusao(Document document, Candidato candidato)
    {
        Texto1(document, candidato);
    }


    private void Texto1(Document document, Candidato candidato)
    {
        bool ehMasculino = candidato.Sexo == Sexo.Masculino;

        #region Titulo
        Titulo(ref document, "Parecer da RHF Talentos");
        #endregion

        var ensino = candidato?.Formacoes?.Where(f => f.Tipo == TipoFormacao.Ensino)
            .OrderByDescending(f => f.DataInicio)
            .ThenByDescending(f => f.DataFim)
            .ThenByDescending(f => f.Ensino)
            .FirstOrDefault();

        Paragrafo(ref document, $"{(ehMasculino ? "Candidato" : "Candidata")} {candidato?.Apelido?.Split(" ")[0]}, {candidato?.Idade} anos, participou pontualmente da entrevista. Está em busca de novas oportunidades profissionais.");

        Espacamento(ref document);

        var texto = $"{(ehMasculino ? "Ele" : "Ela")} é natural de {candidato?.Naturalidade}, ";
        texto += $"reside em {candidato?.Endereco?.Cidade}-{candidato?.Endereco?.Estado} {(string.IsNullOrEmpty(candidato.Endereco.MoraCom) ? string.Empty : $"com {candidato.Endereco.MoraCom}")}. ";
        texto += $"{(ehMasculino ? candidato?.StatusCivil?.Descricao()?.Replace("o(a)", "o") : candidato?.StatusCivil?.Descricao()?.Replace("o(a)", "a"))} e {(candidato?.QuantidadeFilhos == 0 ? "não possui filhos" : $"possui {candidato.QuantidadeFilhos} {(candidato.QuantidadeFilhos > 1 ? "filhos" : "filho")}{(string.IsNullOrEmpty(candidato.IdadeFilhos) ? string.Empty : $" de {candidato.IdadeFilhos}")}")}.";

        Paragrafo(ref document, texto);

        texto = string.Empty;

        if (ensino is not null)
            texto = $"{(ensino?.Status == StatusFormacao.Cursando
            ? $"Esta cursando o {ensino.Ensino?.Descricao()?.ToLower()}"
            : $"Tem o {ensino.Ensino?.Descricao()?.ToLower()}")} " +
            $"{((int)ensino.Ensino.Value > (int)Ensino.Medio ? $"em {ensino.Descricao.ToLower()}" : string.Empty)}" +
            $"{(ensino.Status == StatusFormacao.Concluido ? $" {ensino.Status.Descricao()?.ToLower()}" : string.Empty)}.";

        var cursos = candidato?.Formacoes?.Where(f => f.Tipo == TipoFormacao.Curso).ToList();

        if (cursos!.Any())
            texto += " Possui alguns cursos voltado a área de ";

        for (int i = 0; i < cursos.Count; i++)
        {
            if (string.IsNullOrEmpty(cursos[i].Descricao)) continue;

            if (cursos.Count - 1 == i)
            {
                if (cursos.Count > 1)
                    texto += "e";

                texto += $" {cursos[i].Descricao}. ";
                continue;
            }
            else if (cursos.Count - 2 == i)
            {
                texto += $"{cursos[i].Descricao} ";
                continue;
            }

            texto += $"{cursos[i].Descricao}, ";
        }

        Paragrafo(ref document, texto);
        texto = string.Empty;

        string aspectosProfissionais = string.Empty;

        var experiencias = candidato.Experiencias.OrderBy(f => f.Status).ThenByDescending(f => f.DataFim).ToList();

        if (experiencias.Any())
            aspectosProfissionais += "Em entrevista demonstrou conhecimentos e habilidades nas áreas de ";

        var listHabilidades = experiencias.Select(e => e.Habilidade).ToList();

        List<string> habilidades = new();

        foreach (var habilidade in listHabilidades)
        {
            var arrayHabilidade = habilidade?.Split(",");
            for (int i = 0; i < arrayHabilidade.Length; i++)
                habilidades.Add(arrayHabilidade[i].Trim());
        }

        habilidades = habilidades.Distinct().ToList();

        for (int i = 0; i < habilidades.Count; i++)
        {
            if (string.IsNullOrEmpty(habilidades[i])) continue;

            if (habilidades.Count - 1 == i)
            {
                if (habilidades.Count > 1)
                    texto += "e";

                aspectosProfissionais += $" {habilidades[i]}. ";
                continue;
            }
            else if (habilidades.Count - 2 == i)
            {
                aspectosProfissionais += $"{habilidades[i]} ";
                continue;
            }

            aspectosProfissionais += $"{habilidades[i]}, ";
        }

        var ultimaExperiencia = candidato.Experiencias.OrderBy(f => f.Status).ThenByDescending(f => f.DataFim).FirstOrDefault();

        if (ultimaExperiencia?.UltimoSalario is not null)
            aspectosProfissionais += $"Seu último salário foi de {ultimaExperiencia.UltimoSalario.ParaReais()} na {ultimaExperiencia.Empresa}.";

        Paragrafo(ref document, aspectosProfissionais);

        Espacamento(ref document);
        Paragrafo(ref document, $"Relata ser uma pessoa {candidato.RelatoCandidato}");
        Espacamento(ref document);
        Paragrafo(ref document, $"Em entrevista observam-se características com {candidato.RelatoEntrevistador}. Sendo assim, vejo que {(ehMasculino ? "o candidato" : "a candidata")} possui boas características para dar continuidade no processo.");
        Espacamento(ref document);
        Espacamento(ref document);
        Assinatura(ref document, "Pamela Mitherhofer");
        Assinatura(ref document, "Gerente de RH");
    }
    private void TituloTabelaSemBorda(ref PdfPTable table, string[] valores, int[] colspan)
    {
        if (colspan.Length != valores.Length) throw new ArgumentException("Quantidades de itens no array colspan nao corresponde com a quantidadee do array valores");

        for (int i = 0; i < valores.Length; i++)
        {
            var celulaValor = new CelulaPdf(new Phrase(valores[i], new Font(_rhf.Fonte.GothamBold, _tamanhoFonteTitulo, Font.NORMAL, _rhf.Cor.Azul)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, false)
                .DefinirAlinhamento(Element.ALIGN_CENTER, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 15f, 0);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaValor);
        }
    }
    private void SubTituloTabelaSemBorda(ref PdfPTable table, string[] valores, int[] colspan)
    {
        if (colspan.Length != valores.Length) throw new ArgumentException("Quantidades de itens no array colspan nao corresponde com a quantidadee do array valores");

        for (int i = 0; i < valores.Length; i++)
        {
            var celulaValor = new CelulaPdf(new Phrase(valores[i], new Font(_rhf.Fonte.GothamBold, _tamanhoFonteSubTitulo, Font.NORMAL, _rhf.Cor.Azul)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, false)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 4f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaValor);
        }
    }
    private void LinhaTabelaSemBorda(ref PdfPTable table, string[] valores, int[] colspan)
    {
        if (colspan.Length != valores.Length) throw new ArgumentException("Quantidades de itens no array colspan nao corresponde com a quantidadee do array valores");

        for (int i = 0; i < valores.Length; i++)
        {
            var celulaValor = new CelulaPdf(new Phrase(valores[i], new Font(_rhf.Fonte.GothamMedium, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.CinzaEscuro)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, false)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 4f, 25f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaValor);
        }
    }
    private void PrimeiraLinhaComTopBorder(ref PdfPTable table, string[] titulos, string[] valores, int[] colspan, bool usarBorda = true)
    {
        if (valores.Length != titulos.Length) throw new ArgumentException("Quantidades de itens no array valores nao corresponde com a quantidadee do array titulo");

        if (colspan.Length != titulos.Length) throw new ArgumentException("Quantidades de itens no array colspan nao corresponde com a quantidadee do array titulo");

        for (int i = 0; i < titulos.Length; i++)
        {
            var celulaTitulo = new CelulaPdf(new Phrase(titulos[i], new Font(_rhf.Fonte.GothamBold, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.Preto)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, usarBorda)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(6f, 0, 2f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaTitulo);
        }

        for (int i = 0; i < valores.Length; i++)
        {
            var celulaValor = new CelulaPdf(new Phrase(valores[i], new Font(_rhf.Fonte.GothamMedium, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.CinzaEscuro)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, usarBorda)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 2f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaValor);
        }
    }
    private void LinhaComTopBorder(ref PdfPTable table, string[] titulos, string[] valores, int[] colspan, bool usarBorda = true)
    {
        if (valores.Length != titulos.Length) throw new ArgumentException("Quantidades de itens no array valores nao corresponde com a quantidadee do array titulo");

        if (colspan.Length != titulos.Length) throw new ArgumentException("Quantidades de itens no array colspan nao corresponde com a quantidadee do array titulo");

        for (int i = 0; i < titulos.Length; i++)
        {
            var celulaTitulo = new CelulaPdf(new Phrase(titulos[i], new Font(_rhf.Fonte.GothamBold, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.Preto)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, usarBorda)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 2f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaTitulo);
        }

        for (int i = 0; i < valores.Length; i++)
        {

            var celulaValor = new CelulaPdf(new Phrase(valores[i], new Font(_rhf.Fonte.GothamMedium, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.CinzaEscuro)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, usarBorda)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 2f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaValor);
        }
    }
    private void LinhaComBottomBorder(ref PdfPTable table, string[] titulos, string[] valores, int[] colspan, bool usarBorda = true)
    {
        if (valores.Length != titulos.Length) throw new ArgumentException("Quantidades de itens no array valores nao corresponde com a quantidadee do array titulo");

        if (colspan.Length != titulos.Length) throw new ArgumentException("Quantidades de itens no array colspan nao corresponde com a quantidadee do array titulo");

        for (int i = 0; i < titulos.Length; i++)
        {
            var celulaTitulo = new CelulaPdf(new Phrase(titulos[i], new Font(_rhf.Fonte.GothamBold, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.Preto)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, usarBorda)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 2f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaTitulo);
        }

        for (int i = 0; i < valores.Length; i++)
        {
            var celulaValor = new CelulaPdf(new Phrase(valores[i], new Font(_rhf.Fonte.GothamMedium, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.CinzaEscuro)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.BOTTOM_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, usarBorda)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 2f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaValor);
        }
    }
    private void UltimaLinhaDivisaoTabela(ref PdfPTable table, string[] titulos, string[] valores, int[] colspan)
    {
        if (valores.Length != titulos.Length) throw new ArgumentException("Quantidades de itens no array valores nao corresponde com a quantidadee do array titulo");

        if (colspan.Length != titulos.Length) throw new ArgumentException("Quantidades de itens no array colspan nao corresponde com a quantidadee do array titulo");

        for (int i = 0; i < titulos.Length; i++)
        {
            var celulaTitulo = new CelulaPdf(new Phrase(titulos[i], new Font(_rhf.Fonte.GothamBold, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.Preto)), colspan: colspan[i])
                .DefinirBorda(Rectangle.LEFT_BORDER | Rectangle.TOP_BORDER | Rectangle.RIGHT_BORDER, 0.2f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, false)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 2f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaTitulo);
        }

        for (int i = 0; i < valores.Length; i++)
        {
            var celulaValor = new CelulaPdf(new Phrase(valores[i], new Font(_rhf.Fonte.GothamMedium, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.CinzaEscuro)), colspan: colspan[i])
                .DefinirBorda(Rectangle.BOTTOM_BORDER, 1.5f, _rhf.Cor.Cinza, _rhf.Cor.Cinza, true)
                .DefinirAlinhamento(Element.ALIGN_LEFT, Element.ALIGN_TOP)
                .DefinirPadding(2f, 0, 8f, 5f);

            CelulaPdf.IncluirCelulaDeFraseNaTabela(ref table, celulaValor);
        }
    }

    private void Titulo(ref Document document, string valor)
    {
        Paragraph titulo = new(valor, new Font(_rhf.Fonte.GothamBold, _tamanhoFonteTitulo, Font.NORMAL, _rhf.Cor.Azul))
        {
            Alignment = Element.ALIGN_CENTER,
            SpacingBefore = 2f,
            SpacingAfter = 9f
        };
        document.Add(titulo);
    }
    private void Subtitulo(ref Document document, string valor)
    {
        Paragraph titulo = new(valor, new Font(_rhf.Fonte.GothamBold, _tamanhoFonteSubTitulo, Font.NORMAL, _rhf.Cor.Azul))
        {
            Alignment = Element.ALIGN_LEFT,
            SpacingBefore = 2f,
            SpacingAfter = 9f
        };
        document.Add(titulo);
    }
    private void Paragrafo(ref Document document, string valor)
    {
        Paragraph paragrafo = new(valor, new Font(_rhf.Fonte.GothamMedium, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.CinzaEscuro))
        {
            Alignment = Element.ALIGN_LEFT,
            SpacingBefore = 2f,
            SpacingAfter = 2f,
            IndentationLeft = 30f,
            IndentationRight = 30f
        };
        document.Add(paragrafo);
    }

    private void Espacamento(ref Document document)
    {
        Paragraph paragrafo = new("", new Font(_rhf.Fonte.GothamMedium, _tamanhoFonteText, Font.NORMAL, _rhf.Cor.CinzaEscuro))
        {
            Alignment = Element.ALIGN_LEFT,
            SpacingBefore = 4f,
            SpacingAfter = 4f,
            IndentationLeft = 30f,
            IndentationRight = 30f
        };
        document.Add(paragrafo);
    }
    private void Assinatura(ref Document document, string valor)
    {
        Paragraph paragrafo = new(valor, new Font(_rhf.Fonte.GothamBold, _tamanhoFonteSubTitulo, Font.NORMAL, _rhf.Cor.CinzaEscuro))
        {
            Alignment = Element.ALIGN_RIGHT,
            SpacingBefore = 2f,
            SpacingAfter = 2f,
            IndentationLeft = 30f,
            IndentationRight = 30f
        };
        document.Add(paragrafo);
    }

    private (int anos, int meses) ObterIdade(DateTime dataInicio, DateTime? dataFim)
    {
        if (!dataFim.HasValue)
            dataFim = DateTime.Now;

        if (dataInicio > dataFim) throw new ArgumentException("Data inicio deve ser menor que a data fim.");

        var anos = new DateTime(DateTime.Now.Subtract(dataInicio).Ticks).Year - 1;

        var meses = 0;

        DateTime anosTranscorridos = dataInicio.AddYears(anos);

        for (int i = 1; i <= 12; i++)
        {
            if (anosTranscorridos.AddMonths(i) == DateTime.Now)
            {
                meses = i;
                break;
            }
            else if (anosTranscorridos.AddMonths(i) >= DateTime.Now)
            {
                meses = i - 1;
                break;
            }
        }

        return (anos, meses);
    }


    private void Notificar(string key, string value)
    {
        _notification.Notify(new Notification(LogLevel.Warning, key, value));
    }
}