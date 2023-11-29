using System;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.DTO
{
    public class RoleDTO
    {
        public string RoleName { get; set; }
        public int RoleId { get; set; }

        public RoleDTO(int id, string name)
        {
            RoleId = id;
            RoleName = name;
        }
        public RoleDTO()
        {

        }
    }
}
