using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class MachineDTO
    {
        public int? MachineID { get; set; }
        public string? MachineName { get; set; }
        public List<EventDTO> Events { get; set; } = new();

        public MachineDTO(int id, string name) 
        {
            MachineID = id;
            MachineName = name;
        }
        public MachineDTO()
        {

        }
        public override string ToString()
        {
            return MachineName + ", Machine ID: " + MachineID;
        }
    }
}
