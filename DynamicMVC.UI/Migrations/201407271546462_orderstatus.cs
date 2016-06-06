namespace DynamicMVC.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderstatus : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            Sql("Insert into OrderStatus (Name) Values('New');");
            AddColumn("dbo.Orders", "OrderStatusId", c => c.Int(nullable: false, defaultValue:1));
            AddColumn("dbo.Orders", "OrderDate", c => c.DateTime(nullable: false));
            CreateIndex("dbo.Orders", "OrderStatusId");
            AddForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderStatusId", "dbo.OrderStatus");
            DropIndex("dbo.Orders", new[] { "OrderStatusId" });
            DropColumn("dbo.Orders", "OrderDate");
            DropColumn("dbo.Orders", "OrderStatusId");
            DropTable("dbo.OrderStatus");
        }
    }
}
