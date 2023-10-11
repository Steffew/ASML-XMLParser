using System.Xml;

namespace Business
{
    public class FileService
    {
        public void ReadFileData(Stream stream)
        {
            XmlDocument document = new XmlDocument();
            document.Load(stream);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            nsManager.AddNamespace("ns", "Cimetrix.EDAConnect.E134-0707");

            XmlNodeList eventNodes = document.SelectNodes("/ns:DCP/ns:Event", nsManager);

            foreach (XmlNode eventNode in eventNodes)
            {
                Console.WriteLine($"Event: {eventNode.Attributes["Id"].Value}");
                
                foreach (XmlNode parameterNode in eventNode.SelectNodes("ns:Parameters/ns:Parameter", nsManager))
                {
                    Console.WriteLine($"Parameter: {parameterNode.Attributes["Name"].Value} / {parameterNode.Attributes["SourceId"].Value}");
                }
                
                Console.WriteLine();
            }
        }

        public void SendFileData()
        {
        }
    }
}