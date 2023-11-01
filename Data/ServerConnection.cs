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
            using (sqlConnection)
            {
                sqlConnection.Open();   
                SqlDataReader machinesDataReader = loadMachines.ExecuteReader();
                while (machinesDataReader.Read())
                {
                    DTOs.CreateMachine(machinesDataReader.GetInt32(0), machinesDataReader.GetString(1));
                }
                /* SqlDataReader eventsDataReader = loadEvents.ExecuteReader();
                 while (eventsDataReader.Read())
                {

                }
                SqlDataReader parametersDataReader = loadParameters.ExecuteReader(); */
                sqlConnection.Close();
            }
        }
    }
}