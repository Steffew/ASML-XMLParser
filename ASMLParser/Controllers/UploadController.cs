using ASMLXMLParser.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Xml;

namespace ASMLXMLParser.Controllers
{
    public class UploadController : Controller
    {
        private readonly ILogger<DashboardController> _logger;

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
            //Bron: https://code-maze.com/file-upload-aspnetcore-mvc/
            //Bron: https://www.c-sharpcorner.com/blogs/save-stream-as-file-in-c-sharp
            //Bron: https://www.c-sharpcorner.com/blogs/how-to-select-xml-node-by-name-in-c-sharp
            
                string name = file.FileName.ToString();
                Console.WriteLine($"Naam: {name}");
            
                var stream = file.OpenReadStream();
                    
                 XmlDocument doc = new XmlDocument();
                 doc.Load(stream);

                 XmlNodeList xmlNodeList = doc.SelectNodes("/people/person");
                 
                 foreach (XmlNode xmlNode in xmlNodeList)
                 {
                     string personName = xmlNode["name"].InnerText;
                     string personAge = xmlNode["age"].InnerText;
                     Console.WriteLine($"{personName} + {personAge}");
                 }
                
                return View("Index");
        }
        

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}