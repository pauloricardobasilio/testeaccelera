using Domain;

namespace Infraestructure.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<UnityOfWork>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(UnityOfWork context)
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

            if (context.Colaborators.Any()) return; 

            var fakeColaborator = new Colaborator
            {
                Name = "Test Colaborator",
                DateOfBirth = new DateTime(1990, 01, 01),
                Registry = "11",
                PhoneNumber = "(69) 9999-9999",
                Address = "Fake Street, 11",
                Estate = "FE",
                City = "Fake City"
            };

            var fakeColaborator2 = new Colaborator
            {
                Name = "Test Colaborator 2",
                DateOfBirth = new DateTime(1991, 01, 01),
                Registry = "22",
                PhoneNumber = "(69) 9999-9999",
                Address = "Fake Street, 22",
                Estate = "FE",
                City = "Fake City"
            };

            var fakeColaborator3 = new Colaborator
            {
                Name = "Test Colaborator 3",
                DateOfBirth = new DateTime(1990, 01, 01),
                Registry = "33",
                PhoneNumber = "(69) 9999-9999",
                Address = "Fake Street, 33",
                Estate = "FE",
                City = "Fake City"
            };

            var fakeColaborator4 = new Colaborator
            {
                Name = "Test Colaborator 4",
                DateOfBirth = new DateTime(1990, 01, 01),
                Registry = "44",
                PhoneNumber = "(69) 9999-9999",
                Address = "Fake Street, 44",
                Estate = "FE",
                City = "Fake City"
            };

            context.Colaborators.Add(fakeColaborator);
            context.Colaborators.Add(fakeColaborator2);
            context.Colaborators.Add(fakeColaborator3);
            context.Colaborators.Add(fakeColaborator4);
        }
    }
}
