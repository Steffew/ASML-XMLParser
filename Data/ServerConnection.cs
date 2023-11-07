using Data.DTO;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class ServerConnection
    {
        private static string ConnectionString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi458166_asmleda;TrustServerCertificate=True;Persist Security Info=True;User ID=dbi458166_asmleda;Password=Mr36733duBG2";
        private SqlConnection sqlConnection = new(ConnectionString);

        public void UploadData(SqlCommand uploadCommand)
        {
            sqlConnection.Open();
            uploadCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void LoadData()
        {
            MachineCollection DTOs = new();
            SqlCommand loadMachines = new("SELECT * FROM Machine", sqlConnection);
            SqlCommand loadEvents = new("SELECT * FROM Event", sqlConnection);
            SqlCommand loadParameters = new("SELECT * FROM Parameter", sqlConnection);
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
                SqlDataReader machinesDataReader = LoadAllData.ExecuteReader();
                while (machinesDataReader.Read())
                {
                    machineID = machinesDataReader.GetInt32(0);
                    if (machineID == lastMID)
                    {
                        eventID = machinesDataReader.GetInt32(2);
                        if (eventID == lastEID)
                        {

                        }
                        else
                        {
                            
                            lastEID = eventID;
                        }
                    }
                    else
                    {
                        DTOs.CreateMachine(machineID, machinesDataReader.GetString(1));
                        lastMID = machineID;
                    }
                    
                }
                sqlConnection.Close();
            }
        }
    }
}