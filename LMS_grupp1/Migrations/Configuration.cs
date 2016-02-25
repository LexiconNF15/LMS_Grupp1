namespace LMS_grupp1.Migrations
{
    using LMS_grupp1.Models;
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
