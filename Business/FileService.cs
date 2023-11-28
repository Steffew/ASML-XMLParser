using System.Xml;
using DAL.DTO;

namespace Business
{
    public class FileService
    {
        public void RetrieveFileData(Stream stream)
        {
            XmlDocument document = new XmlDocument();
            document.Load(stream);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            nsManager.AddNamespace("ns", "Cimetrix.EDAConnect");

            XmlNode machineNode = document.SelectSingleNode("/ns:DCP", nsManager);
            XmlNodeList eventNodes = document.SelectNodes("/ns:DCP/ns:Event", nsManager);

            string machineName = machineNode.Attributes["Name"].Value;
            Console.WriteLine($"Machine: {machineName}");

            Machine newMachine = new Machine(machineName);

            foreach (XmlNode eventNode in eventNodes)
            {
                string eventName = eventNode.Attributes["Id"].Value;
                string eventSourceId = eventNode.Attributes["SourceId"].Value;

                Event newEvent = new Event(eventName, eventSourceId);
                newMachine.Events.Add(newEvent);

                Console.WriteLine($"Event: {eventName} / {eventSourceId}");

                foreach (XmlNode parameterNode in eventNode.SelectNodes("ns:Parameters/ns:Parameter", nsManager))
                {
                    string parameterName = parameterNode.Attributes["Name"].Value;
                    string parameterSourceId = parameterNode.Attributes["SourceId"].Value;

                    Parameter newParameter = new Parameter(parameterName, parameterSourceId);
                    newEvent.Parameters.Add(newParameter);

                    Console.WriteLine($"Parameter: {parameterName} / {parameterSourceId}");
                }
                Console.WriteLine();
            }
            MachineService machineService = new MachineService();
            machineService.CreateAndSend(newMachine);
        }
    }
}