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
            using (sqlConnection)
            {
                AllDTOs DTOs = new();
                sqlConnection.Open();
                SqlCommand downloadCommand = new("SELECT * FROM Machine", sqlConnection);
                SqlDataReader machinesDataReader = downloadCommand.ExecuteReader();
                while (machinesDataReader.Read())
                {
                    DTOs.CreateMachine(machinesDataReader.GetInt32(0), machinesDataReader.GetString(1));
                }
                sqlConnection.Close();
            }
        }

    }
}