using ASMLXMLParser.Controllers;
using Business;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Extensions.Logging;
using Moq;

namespace ASML_Unit_Tests
{
    public class Tests
    {
        private Mock<ILogger<DashboardController>> _logger;
        private readonly FileService FileService = new FileService();
        private Mock<IWebHostEnvironment> hostEnv;
        private UploadController uploadcontroller;

        [SetUp]
        public void Setup()
        {
            _logger = new();
            hostEnv = new();
            uploadcontroller = new(_logger.Object, hostEnv.Object);
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
            Assert.IsNull(result.ViewName);
        }
    }
}