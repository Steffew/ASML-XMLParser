using System.Xml;
using DAL;
using DAL.DTO;

namespace Business
{
    public class FileService
    {
        public List<Machine> Machines = new List<Machine>();
        public void RetrieveFileData(Stream stream)
        {
            XmlDocument document = new XmlDocument();
            document.Load(stream);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            nsManager.AddNamespace("ns", "Cimetrix.EDAConnect.E134-0707");

            XmlNodeList eventNodes = document.SelectNodes("/ns:DCP/ns:Event", nsManager);
            //todo: List of machine should be saved and loaded from db.
            Machine newMachine = new Machine();
            
            foreach (XmlNode eventNode in eventNodes)
            {
                string eventId = eventNode.Attributes["Id"].Value;
                
                Event newEvent = new Event(eventId);
                newMachine.Events.Add(newEvent);
                
                Console.WriteLine($"Event: {eventId}");
                
                foreach (XmlNode parameterNode in eventNode.SelectNodes("ns:Parameters/ns:Parameter", nsManager))
                {
                    string name = parameterNode.Attributes["Name"].Value;
                    string sourceId = parameterNode.Attributes["SourceId"].Value;
                    
                    Parameter newParameter = new Parameter(name, sourceId);
                    newEvent.Parameters.Add(newParameter);
                    
                    Console.WriteLine($"Parameter: {name} / {sourceId}");
                }
                
                Console.WriteLine();
            }
            Machines.Add(newMachine);
            Upload dal = new Upload();
            MachineDTO invoer = new MachineDTO(1, "test2");
            dal.UploadMachine(invoer);
        }

        public void SaveFileData()
        {
            
        }
    }
}