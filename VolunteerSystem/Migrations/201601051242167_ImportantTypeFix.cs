namespace VolunteerSystem.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImportantTypeFix : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "State", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "State", c => c.String());
            AlterColumn("dbo.AspNetUsers", "City", c => c.String());
            AlterColumn("dbo.Warehouses", "City", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Warehouses", "City", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "City", c => c.Int(nullable: false));
            AlterColumn("dbo.AspNetUsers", "State", c => c.Int(nullable: false));
            DropColumn("dbo.Warehouses", "State");
            DropColumn("dbo.AspNetUsers", "State");
        }
    }
}
