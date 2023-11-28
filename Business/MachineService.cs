using DAL;
using DAL.DTO;

namespace Business
{
    public class MachineService
    {
        public void CreateAndSend(Machine newMachine)
        {
            MachineDTO machineDto = new MachineDTO();
            machineDto.MachineName = newMachine.Name;
            machineDto.Events = new List<EventDTO>();

            foreach (var _event in newMachine.Events)
            {
                EventDTO newEventDto = new EventDTO(_event.Id, _event.Name, _event.SourceId);
                newEventDto.Parameters = new List<ParameterDTO>();

                foreach (var parameter in _event.Parameters)
                {
                    ParameterDTO newParameterDto = new ParameterDTO(parameter.Id, parameter.Name, parameter.SourceId);
                    newEventDto.Parameters.Add(newParameterDto);
                }

                machineDto.Events.Add(newEventDto);
            }
        }

        public List<Machine> GetAll()
        {
            List<MachineDTO> machineDtos = new List<MachineDTO>();
            ServerConnection serverConnection = new ServerConnection();

            machineDtos = serverConnection.LoadAllData();

            List<Machine> machines = new List<Machine>();

            foreach (MachineDTO machineDto in machineDtos)
            {
                Machine machine = new Machine(machineDto.MachineName);
                machine.Id = machineDto.MachineID;
                machine.Events = new List<Event>();

                foreach (var _event in machineDto.Events)
                {
                    Event newEvent = new Event(_event.EventName, _event.EventSourceID);
                    newEvent.Id = _event.EventID;
                    newEvent.Parameters = new List<Parameter>();

                    foreach (var parameter in _event.Parameters)
                    {
                        Parameter newParameter = new Parameter(parameter.ParameterName, parameter.ParameterSourceID);
                        newParameter.Id = parameter.ParameterID;
                        newEvent.Parameters.Add(newParameter);
                    }

                    machine.Events.Add(newEvent);
                }
                machines.Add(machine);
            }
            return machines;
        }

        public Machine GetById(int id)
        {
            MachineDTO machineDto = new MachineDTO();
            Machine machine = new Machine(machineDto.MachineName);
            machine.Id = machineDto.MachineID;

            foreach (var _event in machineDto.Events)
            {
                Event newEvent = new Event(_event.EventName, _event.EventSourceID);
                newEvent.Id = _event.EventID;
                newEvent.Parameters = new List<Parameter>();

                foreach (var parameter in _event.Parameters)
                {
                    Parameter newParameter = new Parameter(parameter.ParameterName, parameter.ParameterSourceID);
                    newParameter.Id = parameter.ParameterID;
                    newEvent.Parameters.Add(newParameter);
                }

                machine.Events.Add(newEvent);
            }

            return machine;
        }

        public Machine GetByName(string name)
        {
            MachineDTO machineDto = new MachineDTO();
            MachineRepository machineRepository = new MachineRepository();
            machineDto = machineRepository.LoadMachineByName(name);
            Machine machine = new Machine(machineDto.MachineName);
            machine.Id = machineDto.MachineID;

            foreach (var _event in machineDto.Events)
            {
                Event newEvent = new Event(_event.EventName, _event.EventSourceID);
                newEvent.Id = _event.EventID;
                newEvent.Parameters = new List<Parameter>();

                foreach (var parameter in _event.Parameters)
                {
                    Parameter newParameter = new Parameter(parameter.ParameterName, parameter.ParameterSourceID);
                    newParameter.Id = parameter.ParameterID;
                    newEvent.Parameters.Add(newParameter);
                }

                machine.Events.Add(newEvent);
            }

            return machine;
        }

        public int GetTotalMachines(List<Machine> machines)
        {
            return machines.Count;
        }

        public int GetTotalEvents()
        {
            int i = 0;
            ServerConnection serverConnection = new ServerConnection();
            foreach (var machineDto in serverConnection.LoadAllData())
            {
                foreach (var _event in machineDto.Events)
                {
                    i++;
                }
            }

            return i;
        }

        public int GetTotalParameters()
        {
            int j = 0;
            ServerConnection serverConnection = new ServerConnection();
            foreach (var machineDto in serverConnection.LoadAllData())
            {
                foreach (var _event in machineDto.Events)
                {
                    foreach (var parameter in _event.Parameters)
                    {
                        j++;
                    }
                }
            }

            return j;
        }
    }
}