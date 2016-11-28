namespace VolunteerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequestModuleAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceInfoId = c.Int(nullable: false),
                        Count = c.Single(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                        DestinationState = c.String(nullable: false),
                        DestinationCity = c.String(nullable: false),
                        CreationTime = c.DateTime(nullable: false),
                        DestinationTime = c.DateTime(nullable: false),
                        TransitTime = c.Time(nullable: false, precision: 7),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.CustomerRequests");
        }
    }
}
