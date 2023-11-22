using DAL.DTO;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class UserRepository
    {
        ServerConnection con = new();
        SqlConnection sqlConnection;

        public UserRepository()
        {
            sqlConnection = con.GetConnection();
        }

        public List<UserDTO> GetUsers() 
        {
            List<UserDTO> users = new();
            SqlCommand getusers = new("SELECT * FROM [dbo].[User]");
            SqlDataReader dataReader = con.LoadData(getusers);
            sqlConnection.Open();
            while (dataReader.Read()) 
            {
                UserDTO dto = new(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2));
                users.Add(dto);
            }
            sqlConnection.Close();
            return users;
        }
        public List<UserDTO> GetUsersAndRoles() 
        {
            List<UserDTO> users = new();
            SqlCommand getusers = new("SELECT [UserID], [UserName], [Password], Role.RoleName FROM [dbo].[User] INNER JOIN Role on [dbo].[User].[RoleID] = Role.RoleID");
            SqlDataReader dataReader = con.LoadData(getusers);
            sqlConnection.Open();
            while (dataReader.Read()) 
            {
                RoleDTO role = new(dataReader.GetInt32(3), dataReader.GetString(5));
                UserDTO dto = new(dataReader.GetInt32(0), dataReader.GetString(1), dataReader.GetString(2), role);
                users.Add(dto);
            }
            sqlConnection.Close();
            return users;
        }
    }
}
