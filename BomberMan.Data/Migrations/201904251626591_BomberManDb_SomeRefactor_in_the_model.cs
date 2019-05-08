namespace BomberMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BomberManDb_SomeRefactor_in_the_model : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "Score", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "Score");
        }
    }
}
