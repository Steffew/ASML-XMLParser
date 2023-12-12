using DAL.DTO;
using Moq;
using System.Data.SqlClient;

namespace ASML_Unit_Tests
{
    public class ServiceTests
    {
        private FileService testFileService;
        private MemoryStream memoryStream;

        [SetUp]
        public void Setup()
        {
            testFileService = new();           
        }

        public void SetupStream(string FileContent)
        {
            byte[] contentBytes = Encoding.UTF8.GetBytes(FileContent);
            memoryStream = new(contentBytes);
        }

        public bool AssertDatabase()
        {
            MachineRepository repo = new();
            MachineDTO machine = repo.LoadMachineByName("UnitTest");
            if (machine == null)
            {
                return false;
            }
            int id = machine.MachineID;
            repo.RemoveMachineById(id);
            return true;
        }

        public void DisposeDatabase()
        {
            MachineRepository repo = new();
            MachineDTO machine = repo.LoadMachineByName("UnitTest");
            if (machine != null)
            {
                int id = machine.MachineID;
                repo.RemoveMachineById(id);
            }
        }

        [Test]
        public void FileServiceRetrievesFileData()
        {
            // Arrange
            string FileContent = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<DCP Id=\"779075fc-38b0-4d01-ba1d-6cd340da9bd9\" Name=\"UnitTest\" " +
            "Persistent=\"True\" Interval=\"0\" Defined=\"True\" DefinedBy=\"Me\" TimeDefined=\"2012-05-21T13:56:07.028+02:00\"\r\n   " +
            "xmlns=\"Cimetrix.EDAConnect.E134-0707\">\r\n   <Description />\r\n   <Event Id=\"UnitTest\" SourceId=\"UnitTestEvent\">\r\n      " +
            "<Parameters>\r\n         <Parameter Name=\"UnitTestParameter\" SourceId=\"UnitTestParameter\" />\r\n         " +
            "<Parameter Name=\"UnitTestParameter2\" SourceId=\"UnitTestParameter2\" />\r\n      </Parameters>\r\n   </Event>\r\n</DCP>\r\n\r\n";
            SetupStream(FileContent);

            // Act
            testFileService.RetrieveFileData(memoryStream);

            // Assert
            Assert.IsTrue(AssertDatabase());
        }

        [Test]
        public void FileServiceWithoutNamespace()
        {
            // Arrange
            string FileContent = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<DCP Id=\"779075fc-38b0-4d01-ba1d-6cd340da9bd9\" Name=\"UnitTest\" " +
                "Persistent=\"True\" Interval=\"0\" Defined=\"True\" DefinedBy=\"Me\" TimeDefined=\"2012-05-21T13:56:07.028+02:00\">\r\n   " +
                "<Description />\r\n   <Event Id=\"UnitTest\" SourceId=\"UnitTestEvent\">\r\n      <Parameters>\r\n         " +
                "<Parameter Name=\"UnitTestParameter\" SourceId=\"UnitTestParameter\" />\r\n         " +
                "<Parameter Name=\"UnitTestParameter2\" SourceId=\"UnitTestParameter2\" />\r\n      </Parameters>\r\n   </Event>\r\n</DCP>\r\n\r\n";
            SetupStream(FileContent);

            // Assert
            Assert.Throws<System.NullReferenceException>(() => testFileService.RetrieveFileData(memoryStream));
        }
        [Test]
        public void FileServiceWithoutEventsOrParameters()
        {
            // Arrange
            string FileContent = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<DCP Id=\"779075fc-38b0-4d01-ba1d-6cd340da9bd9\" Name=\"UnitTest\" " +
                "Persistent=\"True\" Interval=\"0\" Defined=\"True\" DefinedBy=\"Me\" TimeDefined=\"2012-05-21T13:56:07.028+02:00\"\r\n   " +
                "xmlns=\"Cimetrix.EDAConnect.E134-0707\">\r\n   <Description />\r\n</DCP>";
            SetupStream(FileContent);

            // Act
            testFileService.RetrieveFileData(memoryStream);

            // Assert
            Assert.IsTrue(AssertDatabase());
        }
    }
}
