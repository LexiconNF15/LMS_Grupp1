using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace LMS_grupp1.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        // assigned group
        public int? GroupId { get; set; }
        public virtual Group Group { get; set; }

        public string Name { get; set; }
        public string Personnumber { get; set; }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection2", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Group> Groups { get; set; }


        public System.Collections.IEnumerable AspNetUserRoles { get; set; }

        public System.Collections.IEnumerable AspNetRoles { get; set; }

        public System.Data.Entity.DbSet<LMS_grupp1.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}