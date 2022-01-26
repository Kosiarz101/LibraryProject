namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyArchive : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Archives");
            AddColumn("dbo.Archives", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Archives", "Id");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Archives");
            DropColumn("dbo.Archives", "Id");
            AddPrimaryKey("dbo.Archives", new[] { "ApplicationUserId", "BookId" });
        }
    }
}
