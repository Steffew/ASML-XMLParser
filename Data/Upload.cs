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
            SqlCommand command = new SqlCommand("INSERT INTO dbo.Machine(MachineName) VALUES('" + machine.MachineName + "');");
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
