using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class MachineDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<EventDTO> Events { get; set; }
    }
}
