using DAL.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Load
    {
        ServerConnection con = new();

        public List<MachineDTO> LoadMachineData()
        {
            MachineCollection DTOs = new();
            SqlCommand loadCommand = new("SELECT Machine.MachineID, Machine.MachineName, Event.EventID, Event.EventName, Event.EventSource, " +
                "Parameter.ParameterID, Parameter.ParameterName, Parameter.ParameterSource FROM Machine_Event " +
                "INNER JOIN Event ON Event.EventID = Machine_Event.EventID INNER JOIN Machine on Machine.MachineID = Machine_Event.MachineID " +
                "INNER JOIN Event_Parameter on Event.EventID = Event_Parameter.EventID " +
                "INNER JOIN Parameter on Event_Parameter.ParameterID = Parameter.ParameterID");
            int machineID, eventID;
            int lastMID = 0;
            int lastEID = 0;
            SqlDataReader DataReader = con.LoadData(loadCommand);
            while (DataReader.Read())
            {
                machineID = DataReader.GetInt32(0);
                if (machineID != lastMID)
                {
                    DTOs.AddMachine(machineID, DataReader.GetString(1));
                    lastMID = machineID;
                }
                eventID = DataReader.GetInt32(2);
                if (eventID != lastEID)
                {
                    DTOs.AddEvent(machineID, eventID, DataReader.GetString(3), DataReader.GetString(4));
                    lastEID = eventID;
                }
                DTOs.AddParameter(machineID, eventID, DataReader.GetInt32(5), DataReader.GetString(6), DataReader.GetString(7));
            }
            DTOs.DebugTest();
            return DTOs.machines;
        }
    }
}
