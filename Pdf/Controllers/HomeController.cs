using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Pdf.Utils;
using System.IO;

namespace Pdf.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
         
        /// <summary>
        /// Gera um documento no formato PDF já convertido para base64
        /// </summary>
        /// <returns></returns>
        public ActionResult PdfBase64()
        {
            var pdf = new Utils.Pdf();
            var base64 = pdf.CreatePdf();

            Session["base64"] = base64;

            return View();
        }

        /// <summary>
        /// Disponibiliza o PDF gerado.
        /// </summary>
        /// <returns></returns>
        public FileStreamResult DownloadPdf()
        {
            var base64 = Session["base64"].ToString();
            byte[] data = System.Convert.FromBase64String(base64);

            Response.Clear();
            var ms = new MemoryStream(data);
            Response.ContentType = "application/pdf";
            Response.AddHeader("content-disposition", "attachment;filename=pdf_gerado.pdf");
            Response.Buffer = true;
            ms.WriteTo(Response.OutputStream);
            Response.End();

            return new FileStreamResult(ms, "application/pdf");
        }
    }
}