using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class ParameterDTO
    {
        public int ParameterID { get; set; }
        public string ParameterName { get; set; }
        public string ParameterSourceID { get; set; }
        public List<EventDTO> Events { get; set; }
    }
}
