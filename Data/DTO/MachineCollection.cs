using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class MachineCollection
    {
        public List<MachineDTO> machines = new();

        public void AddMachine(int id, string name)
        {
            MachineDTO newMachine = new(id, name);
            machines.Add(newMachine);
            // test line
            System.Diagnostics.Debug.WriteLine(newMachine);
        }

        public void AddEvents(int machineID, int eventID, string eventName, string eventSource)
        {
            MachineDTO machine = machines.Find(mach => mach.MachineID == machineID);
            EventDTO newEvent = new(eventID, eventName, eventSource);
            machine.Events.Add(newEvent);
        }

        public void AddParameters(int eventID, int parameterID, string parameterName, string ParameterSource)
        {
            ParameterDTO newParameter = new();
        }
    }
}
