using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Pdf.Models;
using System.IO;

namespace Pdf.Utils
{
    public class Pdf
    {
        int headeroneX = 30;
        int headerOney = 25;
        int numeroMaxDeLinhas = 80;
        Double countLines = 0;
        Boolean novaPagina = true;
        List<int> quebraPagina = new List<int>();

        /// <summary>
        /// Cria um documento PDF.
        /// </summary>
        /// <returns></returns>
        public string CreatePdf()
        {
            var queryResult = new Mock().Pdf_Mock();
            var texto = new Mock().Pfg_Mock_Texto();

            PdfDocument document = new PdfDocument();
            PdfPage page = document.AddPage();
            page.Size = PdfSharp.PageSize.Letter;
            page.Orientation = PdfSharp.PageOrientation.Portrait;
            page.Rotate = 0;
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XFont font = new XFont("Verdana", 8, XFontStyle.BoldItalic);
            XFont fontCabecalho = new XFont("Verdana", 12, XFontStyle.BoldItalic);

            document.Info.Title = "Created with PDFsharp";

            ConfigurarQuebraPagina();


            foreach (var item in queryResult)
            {

                gfx = CriarNovaPagina(countLines, page, document, gfx);

                if (novaPagina)
                {
                    gfx.DrawString("HEADER!", fontCabecalho, XBrushes.Black, new XRect(headeroneX, headerOney, page.Width, page.Height), XStringFormats.TopLeft);
                    novaPagina = false;
                    headerOney = headerOney + 50;
                }

                gfx.DrawString(texto, font, XBrushes.Black, new XRect(headeroneX, headerOney, page.Width, page.Height), XStringFormats.TopLeft);

                headerOney = headerOney + 12;
                countLines = countLines + 1;

            }

            var pdf = ConverterPdf(document);

            return pdf;
        }

        /// <summary>
        /// Faz a conversão do PDF em Bytes e depois em Base64
        /// </summary>
        /// <param name="document">Documento PDF pronto para finalizar</param>
        /// <returns></returns>
        private string ConverterPdf(PdfDocument document)
        {
            byte[] fileContents = null;
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream, true);
                fileContents = stream.ToArray();
            }

            var pdf = Convert.ToBase64String(fileContents);

            return pdf;

        }

        /// <summary>
        /// Configura uma nova página e suas medidas.
        /// </summary>
        /// <param name="countLines">Número de linhas</param>
        /// <returns>Retorna um objeto com a configuração da nova página.</returns>
        private XGraphics CriarNovaPagina(Double countLines, PdfPage page, PdfDocument document, XGraphics gfx)
        {
            if (countLines != 0)
            {
                if (countLines % numeroMaxDeLinhas == 0)
                {
                    novaPagina = true;
                    //headeroneX = 30;
                    headerOney = 0;
                    page = document.AddPage();
                    page.Size = PdfSharp.PageSize.Letter;
                    page.Orientation = PdfSharp.PageOrientation.Portrait;
                    page.Rotate = 0;
                    gfx = XGraphics.FromPdfPage(page);
                    //headeroneX = 30;
                    //headerOney = 25;
                    //countLines = 0;
                }
            }
            return gfx;
        }

        /// <summary>
        /// Separa todos os múltiplos do número de linhas do documento.
        /// Cada quebra de página ocorrerá baseando-se neste número.
        /// </summary>
        private void ConfigurarQuebraPagina()
        {
            for (int i = 0; i <= 1000; i++)
            {
                if (i % numeroMaxDeLinhas == 0)
                {
                    quebraPagina.Add(i);
                }

            }
        }
    }
}




//using PdfSharp.Drawing;
//using PdfSharp.Pdf;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using Pdf.Models;
//using System.IO;

//namespace Pdf.Utils
//{
//    public class Pdf
//    {
//        public string CreatePdf()
//        {
//            var queryResult = new Mock().Pdf_Mock();

//            PdfDocument document = new PdfDocument();
//            PdfPage page = document.AddPage();
//            XGraphics gfx = XGraphics.FromPdfPage(page);
//            XFont font = new XFont("Times New Roman", 8, XFontStyle.BoldItalic);

//            document.Info.Title = "Created with PDFsharp";


//            int headeroneX = 30;
//            int headerOney = 25;
//            Int32 countLines = 0;

//            foreach (var item in queryResult)
//            {
//                if (countLines < 20)
//                {
//                    //Header

//                    gfx.DrawString("dsadsadsadas" +
//                    ',' + "dasdsadasdasdsa", font, XBrushes.Black, new XRect(headeroneX, headerOney, page.Width, page.Height), XStringFormats.TopLeft);

//                    headerOney = headerOney + 12;
//                    countLines = countLines + 1;
//                }
//                else if (countLines <= 40)
//                {
//                    if (countLines == 20)
//                    {
//                        //Header
//                        headeroneX = 30;
//                        headerOney = 25;
//                        page = document.AddPage();
//                        gfx = XGraphics.FromPdfPage(page);
//                    }

//                    countLines = countLines + 1;
//                    headerOney = headerOney + 12;

//                    gfx.DrawString("oiiiiiiiiiiiiii" + ',' + "aeeeeeeeeeeeeeeeeeeeeeeeeee", font, XBrushes.Black, new XRect(headeroneX, headerOney, page.Width, page.Height), XStringFormats.TopLeft);

//                }
//            }

//            var pdf = ConverterPdf(document);

//            return pdf;

//        }


//        private string ConverterPdf(PdfDocument document)
//        {
//            byte[] fileContents = null;
//            using (MemoryStream stream = new MemoryStream())
//            {
//                document.Save(stream, true);
//                fileContents = stream.ToArray();
//            }

//            var pdf = Convert.ToBase64String(fileContents);

//            return pdf;

//        }

//        private void CriarNovaPagina()
//        {

//        }
//    }
//}