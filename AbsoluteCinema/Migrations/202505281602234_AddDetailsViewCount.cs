namespace ABSOLUTE_CINEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddDetailsViewCount : DbMigration
    {
        public override void Up()
        {
            
            DropPrimaryKey("dbo.Comments");

            
            AddColumn(
                "dbo.Movies",
                "DetailsViewCount",
                c => c.Int(nullable: false, defaultValue: 0)
            );

            
            AlterColumn("dbo.Movies", "Title", c => c.String());
            AlterColumn("dbo.Movies", "Country", c => c.String());
            AlterColumn("dbo.Movies", "Description", c => c.String());
            AlterColumn("dbo.Comments", "Id", c => c.Guid(nullable: false));
            AlterColumn("dbo.Comments", "Text", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "PasswordHash", c => c.String());

            
            AddPrimaryKey("dbo.Comments", "Id");
        }

        public override void Down()
        {
            
            DropPrimaryKey("dbo.Comments");

            AlterColumn("dbo.Users", "PasswordHash", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Comments", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Movies", "Description", c => c.String(nullable: false));
            AlterColumn("dbo.Movies", "Country", c => c.String(nullable: false, maxLength: 100));
            AlterColumn("dbo.Movies", "Title", c => c.String(nullable: false, maxLength: 200));

           
            DropColumn("dbo.Movies", "DetailsViewCount");

            
            AddPrimaryKey("dbo.Comments", "Id");
        }
    }
}
