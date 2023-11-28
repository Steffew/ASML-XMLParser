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
        SqlConnection sqlConnection;

        public MachineRepository()
        {
            sqlConnection = con.GetConnection();
        }

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
            DataReader.Close();
            sqlConnection.Close();
            DTOs.DebugTest();
            return DTOs.machines;
        }

        public MachineDTO LoadMachineByName(string machineName)
        {
            SqlCommand command = new("SELECT * FROM Machine WHERE MachineName = '" + machineName + "';", sqlConnection);
            MachineDTO machineDTO = new();
            SqlDataReader DataReader = con.LoadData(command);
            sqlConnection.Open();
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
            SqlCommand command = new ("SELECT * FROM Event WHERE MachineName = '" + eventName + "';", sqlConnection);
            EventDTO eventDTO = new ();
            SqlDataReader DataReader = con.LoadData(command);
            sqlConnection.Open();
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
            SqlCommand command = new("SELECT * FROM Parameter WHERE MachineName = '" + parameterName + "';", sqlConnection);
            ParameterDTO parameterDTO = new();
            sqlConnection.Open();
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
            SqlDataReader machineReader = con.LoadData(MachineEventcommand);
            while (machineReader.Read())
            {
                id = machineReader.GetInt32(1);
                EventIDs.Add(id);
            }
            machineReader.Close();
            foreach (int eventID in EventIDs)
            {
                SqlCommand eventCommand = new("SELECT * FROM Event WHERE EventID = " + eventID, sqlConnection);
                SqlDataReader eventReader = con.LoadData(eventCommand);
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
            SqlDataReader eventReader = con.LoadData(eventParameterCommand);
            while (eventReader.Read())
            {
                id = eventReader.GetInt32(1);
                ParameterIDs.Add(id);
            }
            eventReader.Close();
            sqlConnection.Close();
            return dtos;
        }


        public int LatestUploadEventID()
        {
            SqlCommand command = new("SELECT * FROM Event ORDER BY EventId DESC", sqlConnection);
            sqlConnection.Open();
            SqlDataReader dataReader = con.LoadData(command);
            dataReader.Read();
            int id = dataReader.GetInt32(0);
            dataReader.Close();
            sqlConnection.Close();
            return id;
        }

        public int LatestUploadParameterID()
        {
            SqlCommand command = new("SELECT * FROM Parameter ORDER BY ParameterId DESC", sqlConnection);
            sqlConnection.Open();
            SqlDataReader dataReader = con.LoadData(command);
            dataReader.Read();
            int id = dataReader.GetInt32(0);
            dataReader.Close();
            sqlConnection.Close();
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
