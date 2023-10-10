using ASMLXMLParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;
using Business;

namespace ASMLXMLParser.Controllers
{
    public class UploadController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly FileService FileService = new FileService();

        public UploadController(ILogger<DashboardController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        // POST: Upload/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile(IFormFile file)
        {
            try
            {
                string name = file.FileName;
                Console.WriteLine($"Uploaded file name: {name}");

                var stream = file.OpenReadStream();
                FileService.ReadFile(stream);

                return View("Index");
            }
            catch
            {
                return View("Error");
            }
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}