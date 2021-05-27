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

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Interaction>()
            .HasKey(bc => new { bc.ViewerId, bc.PostId });

            //modelBuilder.Entity<Interaction>()
            //    .HasOptional<Viewer>(s => s.Viewer)
            //    .WithMany()
            //    .WillCascadeOnDelete(false);

        }


    }
}