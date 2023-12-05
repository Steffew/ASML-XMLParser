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

        public void SetupStream()
        {
            string FileContent = "<?xml version=\"1.0\" encoding=\"utf-8\" ?>\r\n<DCP Id=\"779075fc-38b0-4d01-ba1d-6cd340da9bd9\" Name=\"UnitTest\" Persistent=\"True\" " +
                "Interval=\"0\" Defined=\"True\" DefinedBy=\"Me\" TimeDefined=\"2012-05-21T13:56:07.028+02:00\"\r\n   xmlns=\"Cimetrix.EDAConnect.E134-0707\">\r\n   " +
                "<Description />\r\n   <Event Id=\"UnitTest\" SourceId=\"TestEvent\">\r\n      <Parameters>\r\n         " +
                "<Parameter Name=\"TestParameter\" SourceId=\"Equipment/Exposure\" />\r\n         <Parameter Name=\"LotStateNr\" SourceId=\"Equipment/Exposure\" />\r\n      " +
                "</Parameters>\r\n   </Event>\r\n</DCP>\r\n\r\n";
            byte[] contentBytes = Encoding.UTF8.GetBytes(FileContent);
            memoryStream = new(contentBytes);
        }

        [Test]
        public void TestFileService()
        {   
            
        }
    }
}
