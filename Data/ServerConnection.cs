using Microsoft.Data.SqlClient;

namespace Data
{
    public class ServerConnection
    {
        private static string ConnectionString = "Data Source=mssqlstud.fhict.local;Initial Catalog=dbi458166_asmleda;Persist Security Info=True;User ID=dbi458166_asmleda;Password=Mr36733duBG2";
        private SqlConnection sqlConnection = new(ConnectionString);

        public void UploadData()
        {

        }

        public void LoadData()
        {

        }

    }
}