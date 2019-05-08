namespace BomberMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BomberManDB_Add_EntityType_to_Player : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "EntityType", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "EntityType");
        }
    }
}
