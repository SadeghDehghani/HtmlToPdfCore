using System.Diagnostics;
using HtmlToPdf.Models;
using Microsoft.AspNetCore.Mvc;

namespace HtmlToPdf.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _env;



        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult List()
        {
            // خواندن لیست فایل‌های HTML از پوشه wwwroot/html
            var folderPath = Path.Combine(_env.WebRootPath, "html");
            var files = Directory.GetFiles(folderPath, "*.html")
                                 .Select(Path.GetFileName)
                                 .ToList();

            return View(files);
        }


        [HttpGet]
        public IActionResult GetHtmlFile(string fileName)
        {
            if (string.IsNullOrEmpty(fileName)) return NotFound();

            var filePath = Path.Combine(_env.WebRootPath, "html", fileName);
            if (!System.IO.File.Exists(filePath)) return NotFound();

            var content = System.IO.File.ReadAllText(filePath);
            return Content(content, "text/html");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
