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
        }

        public void AddEvent(int machineID, int eventID, string eventName, string eventSource)
        {
            MachineDTO machine = machines.Find(mach => mach.MachineID == machineID);
            EventDTO newEvent = new(eventID, eventName, eventSource);
            machine.Events.Add(newEvent);
        }

        public void AddParameter(int machineID, int eventID, int parameterID, string parameterName, string ParameterSource)
        {
            MachineDTO Mach = machines.Find(mach => mach.MachineID == machineID);
            EventDTO eventp = Mach.Events.Find(eventps => eventps.EventID == eventID);
            ParameterDTO newParameter = new(parameterID, parameterName, ParameterSource);
            eventp.Parameters.Add(newParameter);
        }

        public void DebugTest()
        {
            foreach (MachineDTO machine in machines)
            {
                System.Diagnostics.Debug.WriteLine(machine);
                foreach (EventDTO mevent in machine.Events)
                {
                    System.Diagnostics.Debug.WriteLine(mevent);
                    foreach (ParameterDTO parameter in mevent.Parameters)
                    {
                        System.Diagnostics.Debug.WriteLine(parameter);
                    }
                }
            }
        }
    }
}
