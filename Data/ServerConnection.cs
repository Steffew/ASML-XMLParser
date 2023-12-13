using DAL.DTO;
using Microsoft.Data.SqlClient;
using System.Data;
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
            SqlDataReader dataReader = loadCommand.ExecuteReader(CommandBehavior.CloseConnection);
            return dataReader;
        }
    }
}