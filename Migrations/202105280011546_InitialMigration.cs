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
                        Role = c.String(nullable: false),
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
                        Create_date = c.DateTime(nullable: false),
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
                        Id = c.Int(nullable: false, identity: true),
                        PostId = c.Int(nullable: false),
                        ViewerId = c.Int(),
                        IsLike = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Posts", t => t.PostId, cascadeDelete: true)
                .ForeignKey("dbo.Actors", t => t.ViewerId)
                .Index(t => new { t.PostId, t.ViewerId }, unique: true, name: "IX_FirstAndSecond");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interactions", "ViewerId", "dbo.Actors");
            DropForeignKey("dbo.Interactions", "PostId", "dbo.Posts");
            DropForeignKey("dbo.Posts", "EditorId", "dbo.Actors");
            DropIndex("dbo.Interactions", "IX_FirstAndSecond");
            DropIndex("dbo.Posts", new[] { "EditorId" });
            DropIndex("dbo.Actors", new[] { "Username" });
            DropIndex("dbo.Actors", new[] { "Email" });
            DropTable("dbo.Interactions");
            DropTable("dbo.Posts");
            DropTable("dbo.Actors");
        }
    }
}
