namespace DynamicMVC.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customerregion : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerRegions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Customers", "CustomerRegionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Customers", "CustomerRegionId");
            AddForeignKey("dbo.Customers", "CustomerRegionId", "dbo.CustomerRegions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customers", "CustomerRegionId", "dbo.CustomerRegions");
            DropIndex("dbo.Customers", new[] { "CustomerRegionId" });
            DropColumn("dbo.Customers", "CustomerRegionId");
            DropTable("dbo.CustomerRegions");
        }
    }
}
