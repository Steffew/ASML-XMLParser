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

            XmlNodeList? eventNodes = document.SelectNodes("/DCP");
            if (eventNodes != null)
            {
                foreach (XmlNode eventNode in eventNodes)
                {
                    Console.WriteLine($"{eventNode.Attributes["Id"].Value}");
                }
            }
            else
            {
                Console.WriteLine("No event nodes!");
            }

        }

        public void SendFileData() //TODO: invoer is het model bijv.
        {
        }
    }
}