using System.Xml;

namespace Business
{
    public class FileService
    {
        public void ReadFileData(Stream stream) //TODO: return het model bijv.
        {
            // XmlDocument doc = new XmlDocument();
            // doc.Load(stream);
            //
            // XmlNodeList xmlNodeList = doc.SelectNodes("/people/person");
            //
            // foreach (XmlNode xmlNode in xmlNodeList)
            // {
            //     string personName = xmlNode["name"].InnerText;
            //     string personAge = xmlNode["age"].InnerText;
            //     Console.WriteLine($"{personName} + {personAge}");
            // }
            
            XmlDocument document = new XmlDocument();
            document.Load(stream);
            
            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            

            XmlNodeList? eventNodes = document.SelectNodes("/DCP", nsManager);
            if (eventNodes != null)
            {
                foreach (XmlNode eventNode in eventNodes)
                {
                    Console.WriteLine($"Event: {eventNode.Attributes["Id"].Value}");
                }
            }
            else if (eventNodes.Equals(0))
            {
                Console.WriteLine("No event nodes!");
            }

        }

        public void SendFileData() //TODO: invoer is het model bijv.
        {
        }
    }
}