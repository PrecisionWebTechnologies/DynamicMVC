namespace DynamicMVC.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hotorderl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Orders", "IsHotOrder", c => c.Boolean(nullable: false));
            AddColumn("dbo.Orders", "IsReallyHotOrder", c => c.Boolean());
            AddColumn("dbo.Orders", "OrderTypeId", c => c.Int());
            CreateIndex("dbo.Orders", "OrderTypeId");
            AddForeignKey("dbo.Orders", "OrderTypeId", "dbo.OrderTypes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderTypeId", "dbo.OrderTypes");
            DropIndex("dbo.Orders", new[] { "OrderTypeId" });
            DropColumn("dbo.Orders", "OrderTypeId");
            DropColumn("dbo.Orders", "IsReallyHotOrder");
            DropColumn("dbo.Orders", "IsHotOrder");
            DropTable("dbo.OrderTypes");
        }
    }
}
