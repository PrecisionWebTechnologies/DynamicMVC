namespace DynamicMVC.UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class requesteddatetime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "RequestedDateTime", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "RequestedDateTime");
        }
    }
}
