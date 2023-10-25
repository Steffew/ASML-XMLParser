using ASMLXMLParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ASMLXMLParser.Controllers
{
    public class UploadController : Controller
    {
        private readonly ILogger<DashboardController> _logger;
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

        public IActionResult Upload()
        {
            return View();
        }
        public IActionResult UploadFile(IFormFile file)
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
                if(file.Extension != ".xml")
                {
                    onlyXml = false;
                }
            }
            if(onlyXml)
            {
                return View("Upload");
            }
            else
            {
                return View("Upload");
            }
            //string filetype = path.getextension(file.filename).tostring();
            //if (filetype != ".xml")
            //{
            //    viewbag.message = "please only use xml files!";
            //    return view("upload");
            //}
            //else
            //{
            //    string filepath = path.combine(hostenv.webrootpath, filedic);
            //    if (!directory.exists(filepath))
            //    {
            //        directory.createdirectory(filepath);
            //    }
            //    var filename = file.filename;
            //    filepath = path.combine(filepath, filename);
            //    using (filestream fs = system.io.file.create(filepath))
            //    {
            //        file.copyto(fs);
            //    }
            //    return view("upload");
            //}
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}