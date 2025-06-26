using DinkToPdf;
using DinkToPdf.Contracts;

namespace HtmlToPdf.Helpers
{
    public class PdfConverter
    {
        private readonly IConverter _converter;

        public PdfConverter(IConverter converter)
        {
            _converter = converter;
        }

        public byte[] ConvertHtmlToPdf(string htmlContent)
        {
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = PaperKind.A4,
                    Orientation = Orientation.Portrait,
                },
                Objects = {
                    new ObjectSettings()
                    {
                        UseLocalLinks = true,
                        HtmlContent = htmlContent,
                        WebSettings = {
                            DefaultEncoding = "utf-8",
                            UserStyleSheet = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/css/pdf.css")
                        },
                        LoadSettings = {
                            BlockLocalFileAccess = false ,

                        }
                    }
                }
            };

            return _converter.Convert(doc);
        }
    }
}
