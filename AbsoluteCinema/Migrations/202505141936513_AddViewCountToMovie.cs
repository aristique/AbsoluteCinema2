namespace ABSOLUTE_CINEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddViewCountToMovie : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Movies", "ViewCount", c => c.Int(nullable: false, defaultValue: 0));

        }

        public override void Down()
        {
            DropColumn("dbo.Movies", "ViewCount");
        }
    }
}
