using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LMS_grupp1.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<User> Users { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }
}