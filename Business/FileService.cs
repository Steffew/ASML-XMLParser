using System.Xml;
using Data.DTO;

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
                machineDto.Name = newMachine.Name;
                machineDto.Events = new List<EventDTO>();

                foreach (var _event in newMachine.Events)
                {
                    EventDTO newEventDto = new EventDTO();
                    // newEventDto.Id = _event.Id;
                    newEventDto.Name = _event.Name;
                    newEventDto.SourceId = _event.SourceId;
                    newEventDto.Parameters = new List<ParameterDTO>();
                    
                    foreach (var parameter in _event.Parameters)
                    {
                        ParameterDTO newParameterDto = new ParameterDTO();
                        // newParameterDto.Id = parameter.Id;
                        newParameterDto.Name = parameter.Name;
                        newParameterDto.SourceId = parameter.SourceId;
                        newEventDto.Parameters.Add(newParameterDto);
                    }

                    machineDto.Events.Add(newEventDto);
                }

            //MachineRepository.SaveData(machineDto); //TODO: machinerepository method toevoegen.
        }
        


        //Sending file to view layer
        public void GetFileDate(MachineDTO machineDto)
        {
            Machine machine = new Machine(machineDto.Name);
            machine.Id = machineDto.Id;
            machine.Events = new List<Event>();

            foreach (var _event in machineDto.Events)
            {
                Event newEvent = new Event(_event.Name, _event.SourceId);
                newEvent.Id = _event.Id;
                newEvent.Parameters = new List<Parameter>();

                foreach (var parameter in _event.Parameters)
                {
                    Parameter newParameter = new Parameter(parameter.Name, parameter.SourceId);
                    newParameter.Id = parameter.Id;
                    newEvent.Parameters.Add(newParameter);
                }

                machine.Events.Add(newEvent);
            }  
            

        }
        
    }


}