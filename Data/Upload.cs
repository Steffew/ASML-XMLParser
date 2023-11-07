using Data.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{
    public class Upload
    {

        

        public void UploadMachine(MachineDTO machine)
        {
            MachineDTO invoer = new MachineDTO(1, "test");
            string query = $"INSERT INTO Machine (MachineName) VALUES ({invoer.MachineName});";
            SqlCommand command = new SqlCommand(query);
            
        }


        public void UploadEvents(List<EventDTO> events)
        {

        }

        public void UploadParameters()
        {

        }

    }


}
