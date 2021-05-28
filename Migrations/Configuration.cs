namespace press_agency_asp_webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<press_agency_asp_webapp.Models.CodeFContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(press_agency_asp_webapp.Models.CodeFContext context)
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
            //context.UserTypes.Add(new Models.UserType { Name = "admin" });
            //context.UserTypes.Add(new Models.UserType { Name = "editor" });
            //context.UserTypes.Add(new Models.UserType { Name = "viewer" });
            //Models.Editor editor = new Models.Editor
            //{
            //    FirstName = "hesham",
            //    LastName = "ahmed",
            //    Password = "Pass",
            //    Email = "hesham@",
            //    Phone = "Phone",
            //    Username ="hesham",
            //    UserTypeId = 2,
            //    CreateDate = DateTime.Now
            //};
            //context.Actors.Add(editor);

        }
    }
}
