namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddBorrowedBooksAndArchive : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Archives",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        BookId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.BookId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.BookId);
            
            CreateTable(
                "dbo.BorrowedBooks",
                c => new
                    {
                        ApplicationUserId = c.String(nullable: false, maxLength: 128),
                        BookId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUserId, t.BookId })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId, cascadeDelete: true)
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .Index(t => t.ApplicationUserId)
                .Index(t => t.BookId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BorrowedBooks", "BookId", "dbo.Books");
            DropForeignKey("dbo.BorrowedBooks", "ApplicationUserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Archives", "BookId", "dbo.Books");
            DropForeignKey("dbo.Archives", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.BorrowedBooks", new[] { "BookId" });
            DropIndex("dbo.BorrowedBooks", new[] { "ApplicationUserId" });
            DropIndex("dbo.Archives", new[] { "BookId" });
            DropIndex("dbo.Archives", new[] { "ApplicationUserId" });
            DropTable("dbo.BorrowedBooks");
            DropTable("dbo.Archives");
        }
    }
}
