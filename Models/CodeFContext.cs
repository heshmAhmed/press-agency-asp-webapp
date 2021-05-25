using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace press_agency_asp_webapp.Models
{
    public class CodeFContext : DbContext
    {
        public CodeFContext()
            : base("Name=DefaultConnection")
        {
        
        }

        public DbSet<Admin> Admins { get; set; }
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Viewer> Viewers { get; set; }
    }
}