namespace press_agency_asp_webapp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Actors", "ImagePath", c => c.String());
            AddColumn("dbo.Posts", "ImagePath", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "ImagePath");
            DropColumn("dbo.Actors", "ImagePath");
        }
    }
}
