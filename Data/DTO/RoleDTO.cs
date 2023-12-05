using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class RoleDTO
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public RoleDTO(int id, string name) 
        {
            Id = id;
            Name = name;
        }
    }
}
