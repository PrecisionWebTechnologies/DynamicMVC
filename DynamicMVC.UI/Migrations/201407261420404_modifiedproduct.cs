namespace DynamicMVC.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifiedproduct : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "Product_Id", "dbo.Products");
            DropIndex("dbo.Orders", new[] { "Product_Id" });
            AddColumn("dbo.Products", "CreateDate", c => c.DateTime(nullable: false));
            DropColumn("dbo.Orders", "Product_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Orders", "Product_Id", c => c.Int());
            DropColumn("dbo.Products", "CreateDate");
            CreateIndex("dbo.Orders", "Product_Id");
            AddForeignKey("dbo.Orders", "Product_Id", "dbo.Products", "Id");
        }
    }
}
