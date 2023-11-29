using ASMLXMLParser.Controllers;
using Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop.Implementation;
using Moq;
using System.Text;
using System.Xml;

namespace ASML_Unit_Tests
{
    public class Tests
    {
        private Mock<ILogger<DashboardController>> _logger;
        private readonly FileService FileService = new FileService();
        private Mock<IWebHostEnvironment> hostEnv;
        private UploadController uploadcontroller;
        private Mock<IFormFile> mockfile;

        [SetUp]
        public void Setup()
        {
            _logger = new();
            hostEnv = new();
            mockfile = new();
            MockFileSetup();
            uploadcontroller = new(_logger.Object, hostEnv.Object);
            
        }

        public void MockFileSetup()
        {
            var filecontent = "Unit Test";
            var filename = "UnitTest.XML";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(filecontent);
            writer.Flush();
            ms.Position = 0;

            mockfile.Setup(_ => _.OpenReadStream()).Returns(ms);
            mockfile.Setup(_ => _.FileName).Returns(filename);
            mockfile.Setup(_ => _.Length).Returns(ms.Length);
        }

        [Test]
        public void TestUploadView()
        {
            // Arrange
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["Result"] = "success";
            uploadcontroller.TempData = tempData;

            // Act
            var result = uploadcontroller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void TestIfUploadedFilesGetSaved()
        {
            // Arrange
            var file = mockfile.Object;
            var filecontent = "Unit Test";
            var contentBytes = Encoding.UTF8.GetBytes(filecontent);
            var stream = new MemoryStream(contentBytes);
            mockfile.Setup(f => f.OpenReadStream()).Returns(stream);
            hostEnv.Setup(_ => _.WebRootPath).Returns("~\\ASMLParser\\wwwroot");
            string filepath = Path.Combine("~\\ASMLParser\\wwwroot" + "Files");

            // Act 
            var result = uploadcontroller.SaveFiles(file) as ViewResult;

            // Assert
            Assert.IsNull(result);

            using (var reader = new StreamReader(stream))
            {
                var actualContent = reader.ReadToEnd();
                Assert.AreEqual(filecontent, actualContent);
            }
        }

        [Test]
        public void test1() 
        {

        }
    }
}