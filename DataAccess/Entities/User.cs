using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums;

namespace DataAccess.Entities
{
    public class User : Record
    {
        public string Name { get; set; }
        public string Password { get; set; }
        public Status Status { get; set; }
        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
