using Data.DTO;
using Microsoft.Data.SqlClient;

namespace Data
{
    public class ServerConnection
    {
        private static string ConnectionString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi458166_asmleda;Persist Security Info=True;User ID=dbi458166_asmleda;Password=Mr36733duBG2";
        private SqlConnection sqlConnection = new(ConnectionString);

        public void UploadData(SqlCommand uploadCommand)
        {
            sqlConnection.Open();
            uploadCommand.ExecuteNonQuery();
            sqlConnection.Close();
        }

        public void LoadData()
        {
            sqlConnection.Open();
            AllDTOs DTOs = new();
            SqlCommand downloadCommand = new("SELECT * FROM Machine");
            SqlDataReader machinesDataReader = downloadCommand.ExecuteReader();
            while (machinesDataReader.Read())
            {
                DTOs.AddMachine(machinesDataReader.get);
            }
            sqlConnection.Close();
        }

    }
}