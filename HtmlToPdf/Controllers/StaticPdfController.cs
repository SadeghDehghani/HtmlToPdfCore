using DinkToPdf;
using DinkToPdf.Contracts;
using HtmlToPdf.Helpers;
using HtmlToPdf.Services;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToPdf.Controllers
{
    public class StaticPdfController : Controller
    {
        private readonly PdfConverter _pdfConverter;



        public StaticPdfController(PdfConverter pdfConverter)
        {
            _pdfConverter = pdfConverter;
        }

        public IActionResult Generate()
        {
            string html = System.IO.File.ReadAllText(path: "Views/Pdf/HtmlTemplate.html");
        
            var pdfBytes = _pdfConverter.ConvertHtmlToPdf(html);
            return File(pdfBytes, contentType: "application/pdf", fileDownloadName: "output.pdf");
        }
    }
}
