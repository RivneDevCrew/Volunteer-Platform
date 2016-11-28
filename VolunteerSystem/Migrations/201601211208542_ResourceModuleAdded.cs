namespace VolunteerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ResourceModuleAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ResourceInfoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Description = c.String(nullable: false),
                        ResType = c.Int(nullable: false),
                        ResMeasure = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ResWarehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ResourceInfoId = c.Int(nullable: false),
                        Count = c.Single(nullable: false),
                        WarehouseId = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Warehouses", "Capacity", c => c.Single(nullable: false));
            AddColumn("dbo.Warehouses", "StoredResType", c => c.Int(nullable: false));
            AlterColumn("dbo.Warehouses", "Alias", c => c.String(nullable: false));
            AlterColumn("dbo.Warehouses", "Address", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Warehouses", "Address", c => c.String());
            AlterColumn("dbo.Warehouses", "Alias", c => c.String());
            DropColumn("dbo.Warehouses", "StoredResType");
            DropColumn("dbo.Warehouses", "Capacity");
            DropTable("dbo.ResWarehouses");
            DropTable("dbo.ResourceInfoes");
        }
    }
}
