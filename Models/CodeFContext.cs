using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
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
        public DbSet<Editor> Editors { get; set; }
        public DbSet<Viewer> Viewers { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<UserType>  UserTypes { get; set; }
        public DbSet<PostType> PostTypes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Entity<Interaction>()
            .HasKey(i => new { i.PostId, i.ViewerId });

            modelBuilder.Entity<Viewer>()
                .HasMany<Post>(s => s.SavedPosts)
                .WithMany(c => c.ViewerPosts)
                .Map(cs =>
                {
                    cs.MapLeftKey("ViewerId");
                    cs.MapRightKey("PostId");
                    cs.ToTable("SavedPosts");
                    
                });
        }


    }
}