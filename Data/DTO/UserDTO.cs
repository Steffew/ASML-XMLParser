using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class UserDTO
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public string Password { get; set; }
        public RoleDTO Role { get; set; }

        public UserDTO(int id, string name, string password)
        {
            Id = id;
            Name = name;
            Password = password;
        }
    }
}