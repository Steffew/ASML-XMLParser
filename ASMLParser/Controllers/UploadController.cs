using Microsoft.AspNetCore.Mvc;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadFile(IFormFile file)
        {
            try
            {
                if (file == null || file.Length == 0)
                {
                    ViewBag.ErrorMessage = "No file selected!";
                    return View("Index");
                }

                if (!file.ContentType.Equals("text/xml", StringComparison.OrdinalIgnoreCase) &&
                    !file.ContentType.Equals("application/xml", StringComparison.OrdinalIgnoreCase))
                {
                    ViewBag.ErrorMessage = "Invalid file type. Please select an XML file.";
                    return View("Index");
                }

                var stream = file.OpenReadStream();
                FileService.RetrieveFileData(stream);

                return View("Index");
            }
            catch
            {
                ViewBag.ErrorMessage = "An error occurred while processing the file.";
                return View("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
        
        public IActionResult Upload()
        {
            return View();
        }
    }
}