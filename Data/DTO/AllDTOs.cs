using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class AllDTOs
    {
        public List<EventDTO> events = new();
        public List<MachineDTO> machines = new();
        public List<ParameterDTO> parameters = new();

        public void CreateMachine(string name, int id)
        {
            MachineDTO newMachine = new(name, id);
            machines.Add(newMachine);
        }
    }
}
