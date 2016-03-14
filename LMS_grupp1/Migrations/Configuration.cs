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
     },
                      new Group
                      {
                          Id = 4,
                          Name = ".NET April",
                          StartTime = DateTime.ParseExact("04/01/2016", "d", provider),
                          EndTime = DateTime.ParseExact("07/15/2016", "d", provider)
                      },
     new Group
     {
         Id = 5,
         Name = "Java Maj",
         StartTime = DateTime.ParseExact("05/10/2016", "d", provider),
         EndTime = DateTime.ParseExact("08/18/2016", "d", provider)
     },
     new Group
     {
         Id = 6,
         Name = "IT-teknik Juni",
         StartTime = DateTime.ParseExact("06/01/2016", "d", provider),
         EndTime = DateTime.ParseExact("09/18/2016", "d", provider)
     }
);

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
                    GroupId = 1},
                new {Email = "Charles@Schultz.se", 
                    Role= "Student",
                    Name = "Charles Schultz",
                    Personnumber = "920720-1242",
                    PhoneNumber = "070-123458",
                    GroupId = 4},
                new {Email = "Sigvard@Danielsson.se", 
                    Role= "Student",
                    Name = "Sigvard Danielsson",
                    Personnumber = "960720-1212",
                    PhoneNumber = "070-122369",
                    GroupId = 5},
                new {Email = "Ahmed@Dafur.se", 
                    Role= "Student",
                    Name = "Ahmed Dafur",
                    Personnumber = "960801-1212",
                    PhoneNumber = "070-123896",
                    GroupId = 5},
                new {Email = "Ann@Rosenquist.se", 
                    Role= "Student",
                    Name = "Ann Rosenquist",
                    Personnumber = "950120-1262",
                    PhoneNumber = "073-753159",
                    GroupId = 6},
                new {Email = "Richard@Sigvardsson.se", 
                    Role= "Teacher",
                    Name = "Richard Sigvardsson",
                    Personnumber = "630120-1212",
                    PhoneNumber = "070-987654",
                    GroupId = 6}

            
            };
            foreach (var seedUser in seedUsers)
            {
                if (!context.Users.Any(u => u.UserName == seedUser.Email))
                {
                    var user = new ApplicationUser { UserName = seedUser.Email, Email = seedUser.Email };
                    var result = userManager.Create(user, "foobar");
                    //var roleUser = userManager.FindByName(seedUser.Email);
                    if (result.Succeeded) userManager.AddToRole(user.Id, seedUser.Role);
                    user.Personnumber = seedUser.Personnumber;
                    user.PhoneNumber = seedUser.PhoneNumber;
                    user.Name = seedUser.Name;
                    user.GroupId = seedUser.GroupId;

                    //var teacherUser = userManager.FindByName("Kalle@Urbom.se");
                    //userManager.AddToRole(teacherUser.Id, "Teacher");

                    //var studentUser = userManager.FindByName("Nils@Persson.se");
                    //userManager.AddToRole(studentUser.Id, "Student");
                }
                //else
                //{
                //    var user = userManager.FindByEmail(seedUser.Email);
                //    user.Personnumber = seedUser.Personnumber;
                //    user.PhoneNumber = seedUser.PhoneNumber;
                //    user.Name = seedUser.Name;
                //    user.GroupId = seedUser.GroupId;
                //}
            }



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
                     StartTime = DateTime.ParseExact("08/23/2015", "d", provider),
                     EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                 },
                 new Course
                 {
                     Id = 4,
                     GroupId = 4,
                     Description = "Kurs för programmerare.",
                     Name = "C# .NET Påbyggnad",
                     StartTime = DateTime.ParseExact("04/01/2016", "d", provider),
                     EndTime = DateTime.ParseExact("07/15/2016", "d", provider)
                 },
                 new Course
                 {
                     Id = 5,
                     GroupId = 5,
                     Description = "Kurs för Java-programmerare.",
                     Name = "Java Påbyggnad",
                     StartTime = DateTime.ParseExact("05/10/2016", "d", provider),
                     EndTime = DateTime.ParseExact("08/18/2016", "d", provider)
                 },
                 new Course
                 {
                     Id = 6,
                     GroupId = 6,
                     Description = "Kurs för It-tekniker.",
                     Name = "It-Teknik Påbyggnad",
                     StartTime = DateTime.ParseExact("06/01/2016", "d", provider),
                     EndTime = DateTime.ParseExact("09/18/2016", "d", provider)
                 }
            );
            context.Activities.AddOrUpdate(
                a => a.Name,
                new Activity
                {
                    CourseId = 1,
                    Description = "Lära ut C#",
                    Name = "C#",
                    StartTime = DateTime.ParseExact("11/23/2015", "d", provider),
                    EndTime = DateTime.ParseExact("12/18/2015", "d", provider)
                },
                new Activity
                {
                    CourseId = 1,
                    Description = "Att lära ut .NET4.5",
                    Name = ".NET4.5",
                    StartTime = DateTime.ParseExact("12/18/2015", "d", provider),
                    EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 1,
                    Description = "Lära ut mer C#",
                    Name = "C#",
                    StartTime = DateTime.ParseExact("01/18/2016", "d", provider),
                    EndTime = DateTime.ParseExact("02/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 1,
                    Description = "Att lära ut mer .NET4.5",
                    Name = ".NET4.5",
                    StartTime = DateTime.ParseExact("02/18/2016", "d", provider),
                    EndTime = DateTime.ParseExact("03/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 2,
                    Description = "Grundläggande Java",
                    Name = "Java Grund",
                    StartTime = DateTime.ParseExact("03/18/2016", "d", provider),
                    EndTime = DateTime.ParseExact("04/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 2,
                    Description = "Avancerad Java",
                    Name = "Java Avancerad",
                    StartTime = DateTime.ParseExact("04/18/2016", "d", provider),
                    EndTime = DateTime.ParseExact("04/23/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 2,
                    Description = "Mera Java",
                    Name = "Java Påbyggnad",
                    StartTime = DateTime.ParseExact("04/23/2016", "d", provider),
                    EndTime = DateTime.ParseExact("05/01/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 2,
                    Description = "Superavancerad Java",
                    Name = "Java Superavancerad",
                    StartTime = DateTime.ParseExact("05/02/2016", "d", provider),
                    EndTime = DateTime.ParseExact("05/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 3,
                    Description = "Itil för IT-tekniker",
                    Name = "Itil",
                    StartTime = DateTime.ParseExact("08/23/2015", "d", provider),
                    EndTime = DateTime.ParseExact("10/18/2015", "d", provider)
                },
                new Activity
                {
                    CourseId = 3,
                    Description = "SharePoint för IT-tekniker",
                    Name = "Sharepoint",
                    StartTime = DateTime.ParseExact("10/18/2015", "d", provider),
                    EndTime = DateTime.ParseExact("11/18/2015", "d", provider)
                },
                new Activity
                {
                    CourseId = 3,
                    Description = "Påbyggnad för IT-tekniker",
                    Name = "Påbyggnad",
                    StartTime = DateTime.ParseExact("11/18/2015", "d", provider),
                    EndTime = DateTime.ParseExact("12/24/2015", "d", provider)
                },
                new Activity
                {
                    CourseId = 3,
                    Description = "Mera SharePoint för IT-tekniker",
                    Name = "Mera Sharepoint",
                    StartTime = DateTime.ParseExact("12/24/2015", "d", provider),
                    EndTime = DateTime.ParseExact("01/01/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 3,
                    Description = "Itil för IT-tekniker",
                    Name = "Itil",
                    StartTime = DateTime.ParseExact("01/01/2016", "d", provider),
                    EndTime = DateTime.ParseExact("01/09/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 3,
                    Description = "SharePoint för IT-tekniker",
                    Name = "Sharepoint",
                    StartTime = DateTime.ParseExact("01/09/2016", "d", provider),
                    EndTime = DateTime.ParseExact("01/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 5,
                    Description = "TBD",
                    Name = "TBD",
                    StartTime = DateTime.ParseExact("05/10/2016", "d", provider),
                    EndTime = DateTime.ParseExact("05/15/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 5,
                    Description = "TBD",
                    Name = "TBD",
                    StartTime = DateTime.ParseExact("05/15/2016", "d", provider),
                    EndTime = DateTime.ParseExact("08/18/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 4,
                    Description = "Lära ut ännu mer C#",
                    Name = "C#",
                    StartTime = DateTime.ParseExact("04/01/2016", "d", provider),
                    EndTime = DateTime.ParseExact("05/01/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 4,
                    Description = "Att lära ut ännu mera .NET4.5",
                    Name = ".NET4.5",
                    StartTime = DateTime.ParseExact("05/01/2016", "d", provider),
                    EndTime = DateTime.ParseExact("06/01/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 4,
                    Description = "Lära ut jättemycket C#",
                    Name = "C#",
                    StartTime = DateTime.ParseExact("06/01/2016", "d", provider),
                    EndTime = DateTime.ParseExact("06/15/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 4,
                    Description = "Att lära ut jättemycket .NET4.5",
                    Name = ".NET4.5",
                    StartTime = DateTime.ParseExact("06/15/2016", "d", provider),
                    EndTime = DateTime.ParseExact("07/15/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 6,
                    Description = "IT-tekniker",
                    Name = "Sharepoint avancerad",
                    StartTime = DateTime.ParseExact("06/01/2016", "d", provider),
                    EndTime = DateTime.ParseExact("07/01/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 6,
                    Description = "IT-tekniker",
                    Name = "NOC",
                    StartTime = DateTime.ParseExact("07/01/2016", "d", provider),
                    EndTime = DateTime.ParseExact("08/01/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 6,
                    Description = "IT-tekniker",
                    Name = "Avancerad ITIL",
                    StartTime = DateTime.ParseExact("08/01/2016", "d", provider),
                    EndTime = DateTime.ParseExact("08/10/2016", "d", provider)
                },
                new Activity
                {
                    CourseId = 6,
                    Description = "IT-tekniker",
                    Name = "Ännu mera Sharepoint",
                    StartTime = DateTime.ParseExact("08/10/2016", "d", provider),
                    EndTime = DateTime.ParseExact("09/18/2016", "d", provider)
                }
            );
        }
    }
}
