using Microsoft.Data.SqlClient;

namespace Data
{
    public class ServerConnection
    {
        private static string ConnectionString = "Data Source=edaparser.database.windows.net;Initial Catalog=EDA_Parser;User ID=EDA_Manager_Admin;Password=x*79oli*mbJm#8X* ;Connect Timeout=30;Encrypt=True;";
        private SqlConnection sqlConnection = new(ConnectionString);

        public void UploadData()
        {

        }

        public void LoadData()
        {

        }

    }
}