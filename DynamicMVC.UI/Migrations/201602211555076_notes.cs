namespace DynamicMVC.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class notes : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "Notes", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "Notes");
        }
    }
}
