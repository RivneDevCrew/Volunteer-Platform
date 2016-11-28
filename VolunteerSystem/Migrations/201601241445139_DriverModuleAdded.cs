namespace VolunteerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DriverModuleAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DriverId = c.String(),
                        Plate = c.String(nullable: false),
                        CargoMaxWeight = c.Single(nullable: false),
                        ModelDescription = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CustomerRequests", "DriverId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CustomerRequests", "DriverId");
            DropTable("dbo.Cars");
        }
    }
}
