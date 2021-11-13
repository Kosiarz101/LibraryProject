namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AwaitedBooks : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AwaitedBooks",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        BookId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.BookId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.BookId);
            
            AddColumn("dbo.Books", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AwaitedBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.AwaitedBooks", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.AwaitedBooks", new[] { "BookId" });
            DropIndex("dbo.AwaitedBooks", new[] { "ApplicationUserId" });
            DropColumn("dbo.Books", "Quantity");
            DropTable("dbo.AwaitedBooks");
        }
    }
}
