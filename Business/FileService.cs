using System.Xml;
using DAL.DTO;

namespace Business
{
    public class FileService
    {
        //public List<Machine> Machines = new List<Machine>();
        
        // private readonly MachineRepository MachineRepository;
        //
        // public ProductService(MachineRepository machineRepository)
        // {
        //     MachineRepository = machineRepository;
        // }
        
        public void RetrieveFileData(Stream stream)
        {
            XmlDocument document = new XmlDocument();
            document.Load(stream);

            XmlNamespaceManager nsManager = new XmlNamespaceManager(document.NameTable);
            nsManager.AddNamespace("ns", "Cimetrix.EDAConnect.E134-0707");

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
            SaveFileData(newMachine);
        }


        //Sending file to data layer
        public void SaveFileData(Machine newMachine)
        {
            MachineDTO machineDto = new MachineDTO();
            // machineDto.Id = machine.Id;
            machineDto.MachineName = newMachine.Name;
            machineDto.Events = new List<EventDTO>();

            foreach (var _event in newMachine.Events)
            {
                EventDTO newEventDto = new EventDTO(_event.Id, _event.Name, _event.SourceId);
                // newEventDto.Id = _event.Id;
                // newEventDto.EventName = _event.Name;
                // newEventDto.EventSourceID = _event.SourceId;
                newEventDto.Parameters = new List<ParameterDTO>();

                foreach (var parameter in _event.Parameters)
                {
                    ParameterDTO newParameterDto = new ParameterDTO(parameter.Id, parameter.Name, parameter.SourceId);
                    // newParameterDto.Id = parameter.Id;
                    // newParameterDto.ParameterName = parameter.Name;
                    // newParameterDto.ParameterSourceID = parameter.SourceId;
                    newEventDto.Parameters.Add(newParameterDto);
                }

                machineDto.Events.Add(newEventDto);
            }

            //MachineRepository.SaveData(machineDto); //TODO: machinerepository method toevoegen.

        }

    }


}