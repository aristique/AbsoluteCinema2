namespace ABSOLUTE_CINEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movie3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Movies", "YouTubeVideoId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Movies", "YouTubeVideoId", c => c.String(nullable: false));
        }
    }
}
