using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class MachineDTO
    {
        public string MachineName { get; set; }
        public int MachineID { get; set; }
        public List<EventDTO> Events { get; set; }

        public MachineDTO(string name, int id) 
        {
            MachineName = name;
            MachineID = id;

        } 
    }
}
