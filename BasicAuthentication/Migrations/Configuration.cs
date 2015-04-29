namespace BasicAuthentication.Migrations
{
    using BasicAuthentication.Helpers;
    using BasicAuthentication.Infrastructure;
    using BasicAuthentication.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            if (context.users.ToList().Count == 0)
            {
                var newUser = new User()
                {
                    Name = "Carlos",
                    PasswordHash = PasswordHash.HashPassword("forty-two")
                };

                context.users.Add(newUser);
                context.SaveChanges();
            }
        }
    }
}
