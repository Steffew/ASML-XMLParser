using Data.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business
{
    public class MachineService
    {
    //        //TO DO: move SaveFileData to here
    //        //TO DO: Finishing GetAll method


    //        //Getting file from data layer
    //        public void GetAll(MachineDTO machineDto)
    //        {
    //            //Repository repository = new Repository();
    //            //List<MachineDto> machineDtos = repository.GetAll();

    //            List<Machine> machines = new List<Machine>();

    //            foreach (machineDto in machineDtos)
    //            {
    //                Machine machine = new Machine(machineDto.Name);
    //                machine.Id = machineDto.Id;
    //                machine.Events = new List<Event>();

    //                foreach (var _event in machineDto.Events)
    //                {
    //                    Event newEvent = new Event(_event.Name, _event.SourceId);
    //                    newEvent.Id = _event.Id;
    //                    newEvent.Parameters = new List<Parameter>();

    //                    foreach (var parameter in _event.Parameters)
    //                    {
    //                        Parameter newParameter = new Parameter(parameter.Name, parameter.SourceId);
    //                        newParameter.Id = parameter.Id;
    //                        newEvent.Parameters.Add(newParameter);
    //                    }

    //                    machine.Events.Add(newEvent);
    //                }
    //            }

    //        }
    }
}
