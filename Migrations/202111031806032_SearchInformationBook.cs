namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SearchInformationBook : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Books",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Describtion = c.String(nullable: false, maxLength: 550, unicode: false),
                        ISPNNumber = c.Int(nullable: false),
                        Author = c.String(nullable: false, maxLength: 100, unicode: false),
                        CreationDate = c.DateTime(nullable: false),
                        PublicationDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Information",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false, maxLength: 100, unicode: false),
                        Content = c.String(nullable: false, maxLength: 3000, unicode: false),
                        CreationDate = c.DateTime(nullable: false),
                        WasEdited = c.Boolean(nullable: false),
                        LastEditionDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Searches",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(nullable: false, maxLength: 8000, unicode: false),
                        CreationDate = c.DateTime(nullable: false),
                        ApplicationUserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUserId)
                .Index(t => t.ApplicationUserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Searches", "ApplicationUserId", "dbo.AspNetUsers");
            DropIndex("dbo.Searches", new[] { "ApplicationUserId" });
            DropTable("dbo.Searches");
            DropTable("dbo.Information");
            DropTable("dbo.Books");
        }
    }
}
