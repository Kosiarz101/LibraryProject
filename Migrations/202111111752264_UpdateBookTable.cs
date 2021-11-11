namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateBookTable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Books", "Describtion", c => c.String(nullable: false, maxLength: 1100));
            AlterColumn("dbo.Books", "ISPNNumber", c => c.String(nullable: false, maxLength: 13));
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false, maxLength: 200));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Books", "Author", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.Books", "ISPNNumber", c => c.Int(nullable: false));
            AlterColumn("dbo.Books", "Describtion", c => c.String(nullable: false, maxLength: 550, unicode: false));
            AlterColumn("dbo.Books", "Title", c => c.String(nullable: false, maxLength: 100, unicode: false));
        }
    }
}
