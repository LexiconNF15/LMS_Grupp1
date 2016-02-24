using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_grupp1.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Personnumber { get; set; }
        public RoleType Role { get; set; }

        public virtual IEnumerable<RoleType> Roles { get; set; }
    }
}