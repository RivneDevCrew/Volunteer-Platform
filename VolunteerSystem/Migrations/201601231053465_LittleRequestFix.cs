namespace VolunteerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class LittleRequestFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerRequests", "CustomerId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerRequests", "CustomerId", c => c.String(nullable: false));
        }
    }
}
