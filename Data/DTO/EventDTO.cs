using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class EventDTO
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventSource { get; set; }
        public List<ParameterDTO> Parameters { get; set; }
    }
}
