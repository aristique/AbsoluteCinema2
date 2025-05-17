namespace ABSOLUTE_CINEMA.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Movie5 : DbMigration
    {
        public override void Up()
        {
            RenameIndex(table: "dbo.Comments", name: "IX_MovieId", newName: "IX_Comments_MovieId");
            RenameIndex(table: "dbo.Comments", name: "IX_UserId", newName: "IX_Comments_UserId");
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.Comments", "Id", c => c.Guid(nullable: false, identity: true));
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false, maxLength: 500));
            AlterColumn("dbo.Users", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "PasswordHash", c => c.String(nullable: false));
            AddPrimaryKey("dbo.Comments", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.Users", "PasswordHash", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Name", c => c.String());
            AlterColumn("dbo.Comments", "Text", c => c.String());
            AlterColumn("dbo.Comments", "Id", c => c.Guid(nullable: false));
            AddPrimaryKey("dbo.Comments", "Id");
            RenameIndex(table: "dbo.Comments", name: "IX_Comments_UserId", newName: "IX_UserId");
            RenameIndex(table: "dbo.Comments", name: "IX_Comments_MovieId", newName: "IX_MovieId");
        }
    }
}
