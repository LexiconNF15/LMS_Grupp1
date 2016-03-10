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
                new {Email = "Kalle@Urbom.se", 
                    Role= "Teacher",
                    Name = "Kalle Urbom",
                    Personnumber = "520720-1212",
                    PhoneNumber = "070-312345",
                    GroupId = 1},
                new {Email = "Nils@Persson.se", 
                    Role= "Student",
                    Name = "Nils Persson",
                    Personnumber = "020720-1212",
                    PhoneNumber = "070-123456",
                    GroupId = 2},
                new {Email = "Alice@Eriksson.se", 
                    Role= "Student",
                    Name = "Alice Eriksson",
                    Personnumber = "990720-1222",
                    PhoneNumber = "070-789456",
                    GroupId = 1},
                new {Email = "Anna@Ulfsdotter.se", 
                    Role= "Student",
                    Name = "Anna Ulfsdotter",
                    Personnumber = "980720-1212",
                    PhoneNumber = "070-456123",
                    GroupId = 2},
                new {Email = "Orvar@Framsteg.se", 
                    Role= "Student",
                    Name = "Orvar Framsteg",
                    Personnumber = "990712-1212",
                    PhoneNumber = "070-789123",
                    GroupId = 2},
                new {Email = "Tore@Pettersson.se", 
                    Role= "Teacher",
                    Name = "Tore Pettersson",
                    Personnumber = "640720-1212",
                    PhoneNumber = "070-123852",
                    GroupId = 2},
                new {Email = "Ulrika@Vargtass.se", 
                    Role= "Teacher",
                    Name = "Ulrika Vargtass",
                    Personnumber = "580720-1242",
                    PhoneNumber = "070-741258",
                    GroupId = 3},
                new {Email = "Frippe@Danielsson.se", 
                    Role= "Student",
                    Name = "Fredrik Danielsson",
                    Personnumber = "000720-1212",
                    PhoneNumber = "070-852369",
                    GroupId = 3},
                new {Email = "Anneli@Fredriksson.se", 
                    Role= "Student",
                    Name = "Anneli Fredriksson",
                    Personnumber = "010120-1262",
                    PhoneNumber = "070-753159",
                    GroupId = 3},
                new {Email = "Richard@Andersson.se", 
                    Role= "Student",
                    Name = "Richard Andersson",
                    Personnumber = "010120-1212",
                    PhoneNumber = "070-12589",
                    GroupId = 1}

            
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
                }
                else
                {
                    var user = userManager.FindByEmail(seedUser.Email);
                    user.Personnumber = seedUser.Personnumber;
                    user.PhoneNumber = seedUser.PhoneNumber;
                    user.Name = seedUser.Name;
                    user.GroupId = seedUser.GroupId;
                }


            context.Groups.AddOrUpdate(
                p => p.Name,
                 new Group
                 {
                     Id = 1,
                     Name = ".NET November",
                     StartTime = DateTime.ParseExact("11/23/2015", "d", provider),
                     EndTime = DateTime.ParseExact("03/18/2016", "d", provider)
                 },
                 new Group
                 {
                     Id = 2,
                     Name = "Java Februari",
                     StartTime = DateTime.ParseExact("02/23/2016", "d", provider),
                     EndTime = DateTime.ParseExact("05/18/2016", "d", provider)
                 },
                 new Group
                 {
                     Id = 3,
                     Name = ".NET September",
                     StartTime = DateTime.ParseExact("08/23/2015", "d", provider),
                     EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                 }
            );

            context.Courses.AddOrUpdate(
                k => k.Name,
                 new Course
                 {
                     Id = 1,
                     GroupId = 1,
                     Description = "Kurs för programmerare.",
                     Name = "C# .NET ",
                     StartTime = DateTime.ParseExact("11/23/2016", "d", provider),
                     EndTime = DateTime.ParseExact("03/18/2016", "d", provider)
                 },
                 new Course
                 {
                     Id = 2,
                     GroupId = 2,
                     Description = "Kurs för Java-programmerare.",
                     Name = "Java ",
                     StartTime = DateTime.ParseExact("02/23/2016", "d", provider),
                     EndTime = DateTime.ParseExact("05/18/2016", "d", provider)
                 },
                 new Course
                 {
                     Id = 3,
                     GroupId = 3,
                     Description = "Kurs för It-tekniker.",
                     Name = "It-Teknik",
                     StartTime = DateTime.ParseExact("08/23/2016", "d", provider),
                     EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                 }
            );
            context.Activities.AddOrUpdate(
                a => a.Name,
                new Activity
                {
                    CourseId = 1,
                    Description = "Lära ut C#",
                    Name = "C#",
                    StartTime = DateTime.ParseExact("11/23/2016", "d", provider),
                    EndTime = DateTime.ParseExact("03/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 1,
                    Description = "Att lära ut .NET4.5",
                    Name = ".NET4.5",
                    StartTime = DateTime.ParseExact("02/23/2016", "d", provider),
                    EndTime = DateTime.ParseExact("05/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 2,
                    Description = "Grundläggande Java",
                    Name = "Java Grund",
                    StartTime = DateTime.ParseExact("08/23/2016", "d", provider),
                    EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 2,
                    Description = "Avancerad Java",
                    Name = "Java Avancerad",
                    StartTime = DateTime.ParseExact("08/23/2016", "d", provider),
                    EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 3,
                    Description = "Itil för IT-tekniker",
                    Name = "Itil",
                    StartTime = DateTime.ParseExact("08/23/2016", "d", provider),
                    EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 3,
                    Description = "SharePoint för IT-tekniker",
                    Name = "Sharepoint",
                    StartTime = DateTime.ParseExact("08/23/2016", "d", provider),
                    EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                }
            );
/*
            context.DocumentTypes.AddOrUpdate(
                            d => d.Name,
                            new DocumentType
                            {
                                Id = 1,
                                Name = "Term-Paper"
                            },
                             new DocumentType
                             {
                                 Id = 2,
                                 Name = "General Information for The Group"
                             },
                             new DocumentType
                             {
                                 Id = 3,
                                 Name = "Course Document"
                             },
                             new DocumentType
                             {
                                 Id = 4,
                                 Name = "Student Tasks"
                             },
                             new DocumentType
                             {
                                 Id = 5,
                                 Name = "General Information"
                             },
                              new DocumentType
                              {
                                  Id = 6,
                                  Name = "Lecture Notes"
                              }
                       );
*/

        }
    }
}
