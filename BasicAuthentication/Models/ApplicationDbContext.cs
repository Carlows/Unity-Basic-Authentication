using BasicAuthentication.Migrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BasicAuthentication.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : 
            base("BasicAuth")
        {
            // Enable-Migrations -EnableAutomaticMigrations            
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<ApplicationDbContext, Configuration>());
        }

        public DbSet<User> users { get; set; }
    }
}