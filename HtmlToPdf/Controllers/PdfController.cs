using DinkToPdf.Contracts;
using DinkToPdf;
using HtmlToPdf.Models;
using HtmlToPdf.Services;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToPdf.Controllers
{

    public class PdfController : Controller
    {
        private readonly IViewRenderService _viewRenderService;
        private readonly IConverter _converter;

        public PdfController(IViewRenderService viewRenderService, IConverter converter)
        {
            _viewRenderService = viewRenderService;
            _converter = converter;
        }

        public async Task<IActionResult> Invoice()
        {
            var model = new InvoiceViewModel
            {
                InvoiceNumber = "1001",
                Date = "۱۴۰۴/۰۴/۰۶",
                CustomerName = "علی رستگار",
                Items = new List<InvoiceItem>
            {
                new() { Description = "کابل HDMI", Quantity = 2, UnitPrice = 150000 },
                new() { Description = "مانیتور ‌ال جی", Quantity = 1, UnitPrice = 35000000 },
                new() { Description = "موس بی‌سیم", Quantity = 1, UnitPrice = 350000 },
            }
            };

            var html = await _viewRenderService.RenderToStringAsync(viewName: "Pdf/InvoiceTemplate", model);

            var doc = new HtmlToPdfDocument
            {
                GlobalSettings = {
                PaperSize = PaperKind.A4,
                Orientation = Orientation.Landscape
            },
                Objects = {
                new ObjectSettings {
                    HtmlContent = html,
                    WebSettings = { DefaultEncoding = "utf-8", LoadImages = true },
                    LoadSettings = { BlockLocalFileAccess = false }
                }
            }
            };

            var file = _converter.Convert(doc);
            return File(file, contentType: "application/pdf", fileDownloadName: "invoice.pdf");
        }
    }


}
