using DAL.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class MachineRepository
    {
        ServerConnection con = new();
        SqlConnection sqlConnection;

        public MachineRepository()
        {
            sqlConnection = con.GetConnection();
        }


        public MachineDTO LoadMachineByName(string machineName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Machine WHERE MachineName = '" + machineName + "';", sqlConnection);
            MachineDTO machineDTO = new MachineDTO();
            sqlConnection.Open();
            SqlDataReader DataReader = command.ExecuteReader();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    machineDTO.MachineID = DataReader.GetInt32(0);
                    machineDTO.MachineName = DataReader.GetString(1);
                }
            }
            DataReader.Close();
            sqlConnection.Close();
            return machineDTO; 
        }
        public EventDTO LoadEventByName(string eventName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Event WHERE MachineName = '" + eventName + "';", sqlConnection);
            EventDTO eventDTO = new EventDTO();
            sqlConnection.Open();
            SqlDataReader DataReader = command.ExecuteReader();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    eventDTO.EventID = DataReader.GetInt32(0);
                    eventDTO.EventName = DataReader.GetString(1);
                }
            }
            DataReader.Close();
            sqlConnection.Close();
            return eventDTO;
        }
        public ParameterDTO LoadParameterByName(string parameterName)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Parameter WHERE MachineName = '" + parameterName + "';", sqlConnection);
            ParameterDTO parameterDTO = new ParameterDTO();
            sqlConnection.Open();
            SqlDataReader DataReader = command.ExecuteReader();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    parameterDTO.ParameterID = DataReader.GetInt32(0);
                    parameterDTO.ParameterName = DataReader.GetString(1);
                }
            }
            DataReader.Close();
            sqlConnection.Close();
            return parameterDTO;
        }

        public List<EventDTO> LoadEventsByMachineID(int machineID)
        {
            List<EventDTO> dtos = new();
            SqlCommand MachineEventcommand = new("SELECT * FROM Machine_Event WHERE MachineID = " + machineID, sqlConnection);
            List<int> EventIDs = new();
            int id;
            sqlConnection.Open();
            SqlDataReader machineReader = MachineEventcommand.ExecuteReader();
            while(machineReader.Read())
            {
                id = machineReader.GetInt32(1);
                EventIDs.Add(id);
            }
            machineReader.Close();
            foreach (int eventID in EventIDs)
            {
                SqlCommand eventCommand = new("SELECT * FROM Event WHERE EventID = " + eventID, sqlConnection);
                SqlDataReader eventReader = eventCommand.ExecuteReader();
                eventReader.Read();
                EventDTO dto = new(eventReader.GetInt32(0), eventReader.GetString(1), eventReader.GetString(2));
                eventReader.Close();
                dtos.Add(dto);
            }
            sqlConnection.Close();
            return dtos;
        }

        public List<ParameterDTO> LoadParametersByEventID(int eventID)
        {
            List<ParameterDTO> dtos = new();
            SqlCommand eventParameterCommand = new("SELECT * FROM Event_Parameter WHERE EventID = " + eventID, sqlConnection);
            List<int> ParameterIDs = new();
            int id;
            sqlConnection.Open();
            SqlDataReader eventReader = eventParameterCommand.ExecuteReader();
            while (eventReader.Read())
            {
                id = eventReader.GetInt32(1);
                ParameterIDs.Add(id);
            }
            return dtos;
        }


        public int LatestUploadEventID()
        {
            SqlCommand command = new("SELECT * FROM Event ORDER BY ID DESC LIMIT 1");
            sqlConnection.Open();
            int id = (Int32)command.ExecuteScalar();
            return id;
        }
    }
    
}
