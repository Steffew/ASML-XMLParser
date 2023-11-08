using DAL;
using DAL.DTO;

namespace Business
{
    public class MachineService
    {
        //        //TO DO: move SaveFileData to here
        //        //TO DO: Finishing GetAll method


        //Getting file from data layer
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
            }
            return machines;
        }
    }
}
