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
            ServerConnection dal = new ServerConnection();
            MachineRepository machineRepository = new();
            MachineDTO machineCheck = machineRepository.LoadMachineByName(machine.MachineName);
            int machineId;
            if (machineCheck.MachineName == null)
            {
                SqlCommand command = new SqlCommand("INSERT INTO dbo.Machine(MachineName) VALUES('" + machine.MachineName + "');");
                dal.UploadData(command);
                machineCheck = machineRepository.LoadMachineByName(machine.MachineName);
                machineId = machineCheck.MachineID;
            }
            else
            {
                machineId = machine.MachineID;
            }
            foreach(EventDTO eventDTO in machine.Events)
            {

            }
        }


        public void UploadEvents(EventDTO eventDTO)
        {
            
        }

        public void UploadParameters()
        {

        }

    }


}
