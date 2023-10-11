using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Event
    {
        public string EventId { get; set; }
        public string EventName { get; set; }
        public List<Parameter> event_parameterss = new();

    }
}
