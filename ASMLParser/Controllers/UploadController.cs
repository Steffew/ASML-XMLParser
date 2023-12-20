using Microsoft.AspNetCore.Mvc;
using Business;
using System.IO;
using DAL;

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
			ViewBag.Message = TempData["Result"];
			return View();
		}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
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
                if (fileName.Length >= 8 && fileName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    var machineName = fileName.Substring(fileName.Length - 8, 4);
                    var filePath = Path.Combine(FilePath, machineName);

                    using (FileStream fs = System.IO.File.Create(filePath))
                    {
                        file.CopyTo(fs);
                    }

                    FileService.RetrieveFileData(filePath, machineName);
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
            int fileCount = files.Count();
            bool onlyXml = true;

            foreach (FileInfo file in files)
            {
                if (file.Extension.ToLower() == ".xml")
                {
                    string machineName = file.Name.Length >= 8
                        ? file.Name.Substring(file.Name.Length - 8, 4)
                        : file.Name;

                    try
                    {
                        string fileToDeletePath = file.FullName;
                        FileStream stream = file.OpenRead();
                        FileService.RetrieveFileData(fileToDeletePath, machineName);
                        stream.Close();
                        file.Delete();
                        TempData["Result"] = "success";
                    }
                    catch
                    {
                        TempData["Result"] = "An error occurred while processing the file.";
                    }
                }
                else
                {
                    onlyXml = false;
                }
            }

            if (fileCount == 0)
            {
                TempData["Result"] = "nofiles";
            }
            else if (!onlyXml)
            {
                foreach (FileInfo file in files)
                {
                    file.Delete();
                }
                TempData["Result"] = "noxml";
            }

            return RedirectToAction("Index");
        }


        public IActionResult CancelFiles()
        {
            var fileDic = "Files";
            string filePath = Path.Combine(hostEnv.WebRootPath, fileDic);
            DirectoryInfo directory = new DirectoryInfo(filePath);
            FileInfo[] files = directory.GetFiles();
            foreach (FileInfo file in files)
            {
                file.Delete();
            }
            return RedirectToAction("Index");
        }
    }
}