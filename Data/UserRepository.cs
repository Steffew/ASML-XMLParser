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
        public UserDTO LoadUserByName(string username)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM User WHERE UserID = '" + username + "';", sqlConnection);
            UserDTO user = new UserDTO();
            sqlConnection.Open();
            SqlDataReader DataReader = command.ExecuteReader();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    user.Id = DataReader.GetInt32(0);
                    user.Name = DataReader.GetString(1);
                    user.Password = DataReader.GetString(2);
                    int roleId = DataReader.GetInt32(3);
                    RoleDTO role = LoadRoleById(roleId);
                    user.Role = role;
                }
            }
            DataReader.Close();
            sqlConnection.Close();
            return user;
        }
        public RoleDTO LoadRoleById(int id)
        {
            SqlCommand command = new SqlCommand("SELECT * FROM Role WHERE RoleID = '" + id + "';", sqlConnection);
            RoleDTO role = new RoleDTO();
            sqlConnection.Open();
            SqlDataReader DataReader = command.ExecuteReader();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    role.Id = DataReader.GetInt32(0);
                    role.Name = DataReader.GetString(1);
                }
            }
            DataReader.Close();
            sqlConnection.Close();
            return role;
        }
        public List<UserDTO> LoadAllUsers()
        {
            SqlCommand command = new SqlCommand("SELECT * FROM User;", sqlConnection);
            List<UserDTO> users = new List<UserDTO>();
            sqlConnection.Open();
            SqlDataReader DataReader = command.ExecuteReader();
            if (DataReader.HasRows)
            {
                while (DataReader.Read())
                {
                    UserDTO user = new UserDTO();
                    user.Id = DataReader.GetInt32(0);
                    user.Name = DataReader.GetString(1);
                    user.Password = DataReader.GetString(2);
                    int roleId = DataReader.GetInt32(3);
                    RoleDTO role = LoadRoleById(roleId);
                    user.Role = role;
                    users.Add(user);
                }
            }
            DataReader.Close();
            sqlConnection.Close();
            return users;
        }
    }
}
