using Microsoft.AspNetCore.Mvc;
using Business;
using System.IO;

namespace ASMLXMLParser.Controllers
{
    public class UploadController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
        private readonly FileService FileService = new FileService();
        private IWebHostEnvironment hostEnv;
        public UploadController(ILogger<DashboardController> logger, IWebHostEnvironment env)
        {
            _logger = logger;
            hostEnv = env;
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
            ViewBag.Message = TempData["Result"];
            return View();
        }
        public IActionResult SaveFiles(IFormFile file)
        {
            if (file != null && file.Length != 0)
            {
                var fileDic = "Files";
                string FilePath = Path.Combine(hostEnv.WebRootPath, fileDic);
                if (!Directory.Exists(FilePath))
                {
                    Directory.CreateDirectory(FilePath);
                }
                var fileName = file.FileName;
                var filePath = Path.Combine(FilePath, fileName);
                using (FileStream fs = System.IO.File.Create(filePath))
                {
                    file.CopyTo(fs);
                }
            }
            return NoContent();
        }
        public IActionResult ProcessFiles()
        {
            var fileDic = "Files";
            string filePath = Path.Combine(hostEnv.WebRootPath, fileDic);
            DirectoryInfo directory = new DirectoryInfo(filePath);
            FileInfo[] files = directory.GetFiles();
            bool onlyXml = true;
            foreach (FileInfo file in files)
            {
                if (file.Extension != ".xml")
                {
                    onlyXml = false;
                }
            }
            if (onlyXml)
            {
                foreach (FileInfo file in files)
                {
                    try
                    {

                        FileStream stream = file.OpenRead();
                        FileService.RetrieveFileData(stream);
                        stream.Close();
                        file.Delete();
                        TempData["Result"] = "succes";
                    }
                    catch
                    {
                        TempData["Result"] = "An error occurred while processing the file.";
                    }
                }
                return RedirectToAction("Upload");
            }
            else
            {
                TempData["Result"] = "noxml";
                return RedirectToAction("Upload");
            }
        }
    }
}