namespace LibraryProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyQueueAndAwaitedBooks : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AwaitedBooks", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Queues", "CreationDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Queues", "Place", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Queues", "Place");
            DropColumn("dbo.Queues", "CreationDate");
            DropColumn("dbo.AwaitedBooks", "CreationDate");
        }
    }
}
