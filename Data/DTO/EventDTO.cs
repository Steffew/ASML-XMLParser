using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
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

        public EventDTO(int id, string name, string source)
        {
            EventID = id;
            EventName = name;
            EventSource = source;
        }

    }
}
