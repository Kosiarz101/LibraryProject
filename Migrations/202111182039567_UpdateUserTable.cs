namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateUserTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "isSearchSaveModeActivated", c => c.Boolean(nullable: false));
            AddColumn("dbo.Searches", "Type", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Searches", "Type");
            DropColumn("dbo.AspNetUsers", "isSearchSaveModeActivated");
        }
    }
}
