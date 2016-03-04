using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace LMS_grupp1.Models
{
    public class Group
    {
        public int Id { get; set; }
        [DisplayName("Benämning")]
        public string Name { get; set; }

        [DisplayName("Startdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime StartTime { get; set; }

        [DisplayName("Slutdatum")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime EndTime { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
        public virtual ICollection<Document> Documents { get; set; }
    }
}