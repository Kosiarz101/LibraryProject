namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddQueue : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Queues",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Queues", "BookId", "dbo.Books");
            DropForeignKey("dbo.Queues", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Queues", new[] { "BookId" });
            DropIndex("dbo.Queues", new[] { "ApplicationUserId" });
            DropTable("dbo.Queues");
        }
    }
}
