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
        public RoleDTO Role { get; set; }
        public bool IsActive { get; set; }

        public UserDTO(int id, string name, RoleDTO role)
        {
            Id = id;
            Name = name;
            Role = role;
        }
        public UserDTO()
        {

        }
    }
}
