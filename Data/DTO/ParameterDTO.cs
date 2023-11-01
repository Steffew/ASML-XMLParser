﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.DTO
{
    public class ParameterDTO
    {
        public int Id { get; set; }   
        public string Name { get; set; }
        public string SourceId { get; set; }
        public List<EventDTO> Events { get; set; }
    }
}
