using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class EventDTO
    {
        public int EventID { get; set; }
        public string EventName { get; set; }
        public string EventSourceID { get; set; }
        public List<ParameterDTO> Parameters { get; set; } = new();

        public EventDTO(int id, string name, string sourceid)
        {
            EventID = id;
            EventName = name;
            EventSourceID = sourceid;
        }

        public override string ToString()
        {
            return "\tEventID ID: " + EventID + ", Event name: " + EventName + ", Event sourceID: " + EventSourceID;
        }

    }
}
