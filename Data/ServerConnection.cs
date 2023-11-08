using DAL.DTO;
using Microsoft.Data.SqlClient;

namespace DAL
{
    public class ServerConnection
    {
        private static string ConnectionString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi458166_asmleda;TrustServerCertificate=True;Persist Security Info=True;User ID=dbi458166_asmleda;Password=Mr36733duBG2";
        private SqlConnection sqlConnection = new(ConnectionString);

        public void UploadData(SqlCommand uploadCommand)
        {
            sqlConnection.Open();
            uploadCommand.Connection = sqlConnection;
            uploadCommand.ExecuteNonQuery();
            sqlConnection.Close();
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

        public void LoadAllData()
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
            }
        }
    }
}