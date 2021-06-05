using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace press_agency_asp_webapp.Models
{

    public class CodeFContext : DbContext
    {
        private static CodeFContext Instanse = null;

        public CodeFContext(): base("Name=DefaultConnection") { }

        public static CodeFContext CreateCodeFContext()
        {
            if (Instanse == null)
                Instanse = new CodeFContext();
            return Instanse;
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
                .WithMany(c => c.SavedPosts)
                .Map(cs =>
                {
                    cs.MapLeftKey("ViewerId");
                    cs.MapRightKey("PostId");
                    cs.ToTable("SavedPosts");
                    
                });
        }
    }
}