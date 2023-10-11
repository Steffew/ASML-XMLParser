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
        //public async Task<IActionResult> Upload(IFormFile file)
        //{
        //    var fileDic = "Files";
        //    string filePath = Path.Combine(hostEnv.WebRootPath, fileDic);
        //    if(!Directory.Exists(filePath))
        //    {
        //        Directory.CreateDirectory(filePath);
        //    }
        //    var fileName = file.FileName;
        //    filePath = Path.Combine(filePath, fileName);
        //    using (FileStream fs = System.IO.File.Create(filePath))
        //    {
        //        file.CopyTo(fs); 
        //    }
        //    return RedirectToAction("Index");
        //}

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}