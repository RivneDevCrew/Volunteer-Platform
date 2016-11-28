namespace VolunteerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableWHIdFix : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ResWarehouses", "WarehouseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ResWarehouses", "WarehouseId", c => c.Int());
        }
    }
}
