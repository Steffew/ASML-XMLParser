using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
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

        public ParameterDTO(int id, string name, string source)
        {
            ParameterID = id;
            ParameterName = name;
            ParameterSourceID = source;
        }

        public override string ToString()
        {
            return "\t\tParameterID ID: " + ParameterID + ", Parameter name: " + ParameterName + ", Parameter source: " + ParameterSourceID;
        }
    }
}
