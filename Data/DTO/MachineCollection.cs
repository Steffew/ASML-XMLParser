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

        public void CreateMachine(int id, string name)
        {
            MachineDTO newMachine = new(id, name);
            machines.Add(newMachine);
            // test line
            System.Diagnostics.Debug.WriteLine(newMachine);
        }

        public void AddEvents(int machineID, int eventID, string eventName, string eventSource)
        {
//            MachineDTO machine = machines.
        }
    }
}
