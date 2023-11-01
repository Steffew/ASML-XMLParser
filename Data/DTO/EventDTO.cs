using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class EventDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceId { get; set; }
        
        
        public List<ParameterDTO> Parameters { get; set; }
    }
}
