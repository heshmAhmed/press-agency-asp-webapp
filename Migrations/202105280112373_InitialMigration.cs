namespace press_agency_asp_webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        Password = c.String(nullable: false),
                        Email = c.String(nullable: false, maxLength: 450),
                        Phone = c.String(nullable: false, maxLength: 11),
                        Type = c.String(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        Username = c.String(maxLength: 450),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Email, unique: true)
                .Index(t => t.Username, unique: true);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        No_views = c.Int(nullable: false),
                        No_likes = c.Int(nullable: false),
                        No_dislikes = c.Int(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                        State = c.Boolean(nullable: false),
                        Type = c.String(nullable: false),
                        EditorId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Actors", t => t.EditorId, cascadeDelete: true)
                .Index(t => t.EditorId);
            
            CreateTable(
                "dbo.Interactions",
                c => new
                    {
                        PostId = c.Int(nullable: false),
                        ViewerId = c.Int(nullable: false),
                        IsLike = c.Boolean(nullable: false),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.PostId, t.ViewerId })
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.ViewerId)
                .Index(t => t.PostId)
                .Index(t => t.ViewerId);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        ViewerId = c.Int(nullable: false),
                        Title = c.String(nullable: false),
                        Body = c.String(nullable: false),
                        Answer = c.String(),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.ViewerId)
                .Index(t => t.PostId)
                .Index(t => t.ViewerId);
            
            CreateTable(
                "dbo.SavedPosts",
                c => new
                    {
                        ViewerId = c.Int(nullable: false),
                        PostId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ViewerId, t.PostId })
                .ForeignKey("dbo.Actors", t => t.ViewerId)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .Index(t => t.ViewerId)
                .Index(t => t.PostId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SavedPosts", "PostId", "dbo.Posts");
            DropForeignKey("dbo.SavedPosts", "ViewerId", "dbo.Actors");
            DropForeignKey("dbo.Questions", "ViewerId", "dbo.Actors");
            DropForeignKey("dbo.Questions", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Interactions", "ViewerId", "dbo.Actors");
            DropForeignKey("dbo.Interactions", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "EditorId", "dbo.Actors");
            DropIndex("dbo.SavedPosts", new[] { "PostId" });
            DropIndex("dbo.SavedPosts", new[] { "ViewerId" });
            DropIndex("dbo.Questions", new[] { "ViewerId" });
            DropIndex("dbo.Questions", new[] { "PostId" });
            DropIndex("dbo.Interactions", new[] { "ViewerId" });
            DropIndex("dbo.Interactions", new[] { "PostId" });
            DropIndex("dbo.Posts", new[] { "EditorId" });
            DropIndex("dbo.Actors", new[] { "Username" });
            DropIndex("dbo.Actors", new[] { "Email" });
            DropTable("dbo.SavedPosts");
            DropTable("dbo.Questions");
            DropTable("dbo.Interactions");
            DropTable("dbo.Posts");
            DropTable("dbo.Actors");
        }
    }
}
