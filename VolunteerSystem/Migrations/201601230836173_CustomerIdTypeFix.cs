namespace VolunteerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CustomerIdTypeFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CustomerRequests", "CustomerId", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CustomerRequests", "CustomerId", c => c.Int(nullable: false));
        }
    }
}
