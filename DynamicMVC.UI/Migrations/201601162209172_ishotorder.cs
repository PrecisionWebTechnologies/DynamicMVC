namespace DynamicMVC.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ishotorder : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "IsHotOrder", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "IsHotOrder");
        }
    }
}
