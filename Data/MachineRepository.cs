using DAL.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MachineRepository
    {
        ServerConnection con = new();

        public List<MachineDTO> LoadAllMachines()
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

        public MachineDTO LoadMachineByName(string machineName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Machine WHERE MachineName = '" + machineName + "';");
            MachineDTO machineDTO = new MachineDTO();
            SqlDataReader DataReader = con.LoadData(command);
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    machineDTO.MachineID = DataReader.GetInt32(0);
                    machineDTO.MachineName = DataReader.GetString(1);
                }
            }
            DataReader.Close();
            return machineDTO; 
        }
        public EventDTO LoadEventByName(string eventName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Event WHERE MachineName = '" + eventName + "';");
            EventDTO eventDTO = new EventDTO();
            SqlDataReader DataReader = con.LoadData(command);
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    eventDTO.EventID = DataReader.GetInt32(0);
                    eventDTO.EventName = DataReader.GetString(1);
                }
            }
            DataReader.Close();
            return eventDTO;
        }
        public ParameterDTO LoadParameterByName(string parameterName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Parameter WHERE MachineName = '" + parameterName + "';");
            ParameterDTO parameterDTO = new ParameterDTO();
            SqlDataReader DataReader = con.LoadData(command);
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    parameterDTO.ParameterID = DataReader.GetInt32(0);
                    parameterDTO.ParameterName = DataReader.GetString(1);
                }
            }
            DataReader.Close();
            return parameterDTO;
        }

        public List<EventDTO> LoadEventsByMachineID(int machineID)
        {
            List<EventDTO> dtos = new();
            SqlCommand MachineEventcommand = new("SELECT * FROM Machine_Event WHERE MachineID = " + machineID);
            List<int> EventIDs = new();
            int id;
            SqlDataReader machineReader = con.LoadData(MachineEventcommand);
            while(machineReader.Read())
            {
                id = machineReader.GetInt32(1);
                EventIDs.Add(id);
            }
            machineReader.Close();
            foreach (int eventID in EventIDs)
            {
                SqlCommand eventCommand = new("SELECT * FROM Event WHERE EventID = " + eventID);
                SqlDataReader eventReader = con.LoadData(eventCommand);
                eventReader.Read();
                EventDTO dto = new(eventReader.GetInt32(0), eventReader.GetString(1), eventReader.GetString(2));
                eventReader.Close();
                dtos.Add(dto);
            }
            return dtos;
        }

        public List<ParameterDTO> LoadParametersByEventID(int eventID)
        {
            List<ParameterDTO> dtos = new();
            SqlCommand eventParameterCommand = new("SELECT * FROM Event_Parameter WHERE EventID = " + eventID);
            List<int> ParameterIDs = new();
            int id;
            SqlDataReader eventReader = con.LoadData(eventParameterCommand);
            while (eventReader.Read())
            {
                id = eventReader.GetInt32(1);
                ParameterIDs.Add(id);
            }
            return dtos;
        }


        public int LatestUploadEventID()
        {
            SqlCommand command = new("SELECT * FROM Event ORDER BY EventId DESC");
            SqlDataReader dataReader = con.LoadData(command);
            dataReader.Read();
            int id = dataReader.GetInt32(0);
            return id;
        }

        public int LatestUploadParameterID()
        {
            SqlCommand command = new("SELECT * FROM Parameter ORDER BY ParameterId DESC");
            SqlDataReader dataReader = con.LoadData(command);
            dataReader.Read();
            int id = dataReader.GetInt32(0);
            return id;
        }

        public void RemoveMachineById(int machineid)
        {
            SqlCommand deleteProcedure = new("DeleteMachineAndChildren");
            deleteProcedure.CommandType = CommandType.StoredProcedure;
            deleteProcedure.Parameters.AddWithValue("@MachineID", machineid);
            con.UploadData(deleteProcedure);
        }
    }
    
}
