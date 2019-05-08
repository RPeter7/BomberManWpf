namespace BomberMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BomberManDB_Change_Map_To_MapElements_And_Some_Column : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Maps", "Id", "dbo.GameStates");
            DropIndex("dbo.Maps", new[] { "Id" });
            CreateTable(
                "dbo.MapElements",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        X = c.Int(nullable: false),
                        Y = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        GameStateId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameStates", t => t.GameStateId, cascadeDelete: true)
                .Index(t => t.GameStateId);
            
            DropTable("dbo.Maps");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Coordinate = c.String(),
                        Type = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.MapElements", "GameStateId", "dbo.GameStates");
            DropIndex("dbo.MapElements", new[] { "GameStateId" });
            DropTable("dbo.MapElements");
            CreateIndex("dbo.Maps", "Id");
            AddForeignKey("dbo.Maps", "Id", "dbo.GameStates", "Id");
        }
    }
}
