using DAL.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace DAL
{
    public class Upload
    {
        public void UploadMachine(MachineDTO machine)
        {
            string query = $"INSERT INTO Machine (MachineName) VALUES ({machine.MachineName});";
            SqlCommand command = new SqlCommand(query);
            ServerConnection dal = new ServerConnection();
            dal.UploadData(command);
        }


        public void UploadEvents(List<EventDTO> events)
        {

        }

        public void UploadParameters()
        {

        }

    }


}
