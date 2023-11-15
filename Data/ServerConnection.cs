using DAL.DTO;
using Microsoft.Data.SqlClient;
using System.Reflection.PortableExecutable;

namespace DAL
{
    public class ServerConnection
    {
        private static string ConnectionString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi458166_asmleda;TrustServerCertificate=True;Persist Security Info=True;User ID=dbi458166_asmleda;Password=Mr36733duBG2";
        private SqlConnection sqlConnection = new(ConnectionString);

        public SqlConnection GetConnection()
        {
            return sqlConnection;
        }

        public void UploadData(SqlCommand uploadCommand)
        {
            sqlConnection.Open();
            uploadCommand.Connection = sqlConnection;
            uploadCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public SqlDataReader LoadData(SqlCommand loadCommand)
        {
            sqlConnection.Open();
            loadCommand.Connection = sqlConnection;
            SqlDataReader dataReader = loadCommand.ExecuteReader();
            sqlConnection.Close();
            return dataReader;
        }

        public List<MachineDTO> LoadAllData()
        {
            MachineCollection DTOs = new();
            SqlCommand LoadAllData = new("SELECT Machine.MachineID, Machine.MachineName, Event.EventID, Event.EventName, Event.EventSource, " +
                "Parameter.ParameterID, Parameter.ParameterName, Parameter.ParameterSource FROM Machine_Event " +
                "INNER JOIN Event ON Event.EventID = Machine_Event.EventID INNER JOIN Machine on Machine.MachineID = Machine_Event.MachineID " +
                "INNER JOIN Event_Parameter on Event.EventID = Event_Parameter.EventID " +
                "INNER JOIN Parameter on Event_Parameter.ParameterID = Parameter.ParameterID", sqlConnection);
            int machineID, eventID;
            int lastMID = 0;
            int lastEID = 0;
            using (sqlConnection)
            {
                sqlConnection.Open();   
                SqlDataReader DataReader = LoadAllData.ExecuteReader();
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
                sqlConnection.Close();
                return DTOs.machines;
            }
        }
    }
}