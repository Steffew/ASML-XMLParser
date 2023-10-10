using System.Xml;

namespace Business
{
    public class FileService
    {
        public void ReadFile(Stream stream) //TODO: return het model bijv.
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(stream);

            XmlNodeList xmlNodeList = doc.SelectNodes("/people/person");

            foreach (XmlNode xmlNode in xmlNodeList)
            {
                string personName = xmlNode["name"].InnerText;
                string personAge = xmlNode["age"].InnerText;
                Console.WriteLine($"{personName} + {personAge}");
            }
        }

        public void SendFileData() //TODO: invoer is het model bijv.
        {
        }
    }
}