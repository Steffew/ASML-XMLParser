using Moq;

namespace ASML_Unit_Tests
{
    public class Tests
    {
        private Mock<ILogger<DashboardController>> _logger;
        private FileService serviceTest;
        private Mock<IWebHostEnvironment> hostEnv;
        private UploadController uploadcontroller;
        private Mock<IFormFile> mockfile;

        [SetUp]
        public void Setup()
        {
            serviceTest = new();
            _logger = new();
            hostEnv = new();
            mockfile = new();
            MockFileSetup();
            var tempData = HostEnvSetup();
            uploadcontroller = new(_logger.Object, hostEnv.Object);
            uploadcontroller.TempData = tempData;
        }

        public void MockFileSetup()
        {
            var filecontent = "Unit Test";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(filecontent);
            writer.Flush();
            ms.Position = 0;

            mockfile.Setup(_ => _.OpenReadStream()).Returns(ms);
            mockfile.Setup(_ => _.Length).Returns(ms.Length);
        }

        public TempDataDictionary HostEnvSetup()
        {
            hostEnv.Setup(_ => _.WebRootPath).Returns("~\\ASMLParser\\wwwroot");
            var httpContext = new DefaultHttpContext();
            var tempData = new TempDataDictionary(httpContext, Mock.Of<ITempDataProvider>());
            tempData["Result"] = "success";
            return tempData;
        }

        [Test]
        public void TestIfControllerLoads()
        {
            // Act
            var result = uploadcontroller.Index() as ViewResult;

            // Assert
            Assert.IsInstanceOf<IActionResult>(result);
        }

        [Test]
        public void TestIfUploadedFilesGetSaved()
        {
            // Arrange
            var filename = "Test.xml";
            mockfile.Setup(_ => _.FileName).Returns(filename);
            var file = mockfile.Object;
            var filecontent = "Unit Test";
            var contentBytes = Encoding.UTF8.GetBytes(filecontent);
            var stream = new MemoryStream(contentBytes);
            mockfile.Setup(f => f.OpenReadStream()).Returns(stream);
            string filepath = Path.Combine("~\\ASMLParser\\wwwroot" + "Files");

            // Act 
            var result = uploadcontroller.SaveFiles(file) as ViewResult;

            // Assert
            Assert.IsNull(result);

            using (var reader = new StreamReader(stream))
            {
                var actualContent = reader.ReadToEnd();
                Assert.AreEqual(actualContent, filecontent);
            }
        }

        [Test]
        public void UploadWithNoFiles() 
        {
            // Act
            var result = uploadcontroller.ProcessFiles();
            var tempdata = uploadcontroller.TempData;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("nofiles", tempdata["Result"]);
        }

        [Test]
        public void UploadFileThatsNotAnXml()
        {
            // Arrange
            var filename = "NotAnXml.pdf";
            mockfile.Setup(_ => _.FileName).Returns(filename);
            var file = mockfile.Object;

            // Act 
            uploadcontroller.SaveFiles(file);
            var result = uploadcontroller.ProcessFiles();
            var tempdata = uploadcontroller.TempData;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("noxml", tempdata["Result"]);

        }

        [Test]
        public void CancelFilesTest()
        {
            // Arrange
            var filename = "Test.xml";
            mockfile.Setup(_ => _.FileName).Returns(filename);
            var file = mockfile.Object;
            var filecontent = "Unit Test";
            var contentBytes = Encoding.UTF8.GetBytes(filecontent);
            var stream = new MemoryStream(contentBytes);
            mockfile.Setup(f => f.OpenReadStream()).Returns(stream);
            string filepath = Path.Combine("~\\ASMLParser\\wwwroot" + "Files");

            // Act
            uploadcontroller.SaveFiles(file);
            var result = uploadcontroller.CancelFiles();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsFalse(File.Exists(filepath));
        }

        // Could potentially test uploading a file, but not sure if that's necessary
    }
}