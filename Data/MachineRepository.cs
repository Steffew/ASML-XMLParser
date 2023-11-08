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
        public MachineDTO LoadMachineByName(string machineName)
        {
            ServerConnection con = new();
            SqlConnection sqlConnection = con.GetConnection();
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
            ServerConnection con = new();
            SqlConnection sqlConnection = con.GetConnection();
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
            ServerConnection con = new();
            SqlConnection sqlConnection = con.GetConnection();
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
    }
    
}
