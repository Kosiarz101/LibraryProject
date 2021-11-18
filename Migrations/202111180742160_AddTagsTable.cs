namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddTagsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BookTags",
                c => new
                    {
                        BookId = c.Int(nullable: false),
                        TagName = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => new { t.BookId, t.TagName })
                .ForeignKey("dbo.Books", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagName, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.TagName);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Name = c.String(nullable: false, maxLength: 200),
                    })
                .PrimaryKey(t => t.Name);
            
            AlterColumn("dbo.Searches", "Content", c => c.String(nullable: false, maxLength: 1000, unicode: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.BookTags", "TagName", "dbo.Tags");
            DropForeignKey("dbo.BookTags", "BookId", "dbo.Books");
            DropIndex("dbo.BookTags", new[] { "TagName" });
            DropIndex("dbo.BookTags", new[] { "BookId" });
            AlterColumn("dbo.Searches", "Content", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            DropTable("dbo.Tags");
            DropTable("dbo.BookTags");
        }
    }
}
