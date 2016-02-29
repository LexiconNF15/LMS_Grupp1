namespace LMS_grupp1.Migrations
{
    using LMS_grupp1.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LMS_grupp1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            ContextKey = "LMS_grupp1.Models.ApplicationDbContext";
        }

        protected override void Seed(LMS_grupp1.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
            CultureInfo provider = CultureInfo.InvariantCulture;

            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            foreach (string roleName in new[] { "Teacher", "Student" })
            {
                if (!context.Roles.Any(r => r.Name == roleName))
                {
                    var role = new IdentityRole { Name = roleName };
                    roleManager.Create(role);
                }
            }


            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);


            var seedUsers = new[] { 
                new {Email = "Kalle@Urbom.se", Role= "Teacher"},
                new {Email = "Nils@Persson.se", Role= "Student"},
                new {Email = "Alice@Eriksson.se", Role= "Student"},
                new {Email = "Anna@Ulfsdotter.se", Role= "Student"},
                new {Email = "Orvar@Framsteg.se", Role= "Student"},
                new {Email = "Tore@Pettersson.se", Role= "Teacher"}

            
            };
            foreach (var seedUser in seedUsers)
                if (!context.Users.Any(u => u.UserName == seedUser.Email))
                {
                    var user = new ApplicationUser { UserName = seedUser.Email, Email = seedUser.Email };
                    var result = userManager.Create(user, "foobar");
                    //var roleUser = userManager.FindByName(seedUser.Email);
                    if (result.Succeeded) userManager.AddToRole(user.Id, seedUser.Role);


                    //var teacherUser = userManager.FindByName("Kalle@Urbom.se");
                    //userManager.AddToRole(teacherUser.Id, "Teacher");

                    //var studentUser = userManager.FindByName("Nils@Persson.se");
                    //userManager.AddToRole(studentUser.Id, "Student");


                    context.Groups.AddOrUpdate(
                        p => p.Name,
                         new Group
                         {
                             Name = ".NET November",
                             StartTime = DateTime.ParseExact("11/23/2016", "d", provider),
                             EndTime = DateTime.ParseExact("03/18/2016", "d", provider)
                         },
                         new Group
                         {
                             Name = "Java Februari",
                             StartTime = DateTime.ParseExact("02/23/2016", "d", provider),
                             EndTime = DateTime.ParseExact("05/18/2016", "d", provider)
                         },
                         new Group
                         {
                             Name = ".NET September",
                             StartTime = DateTime.ParseExact("08/23/2016", "d", provider),
                             EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                         }
        );
                }
        }
    }
}
