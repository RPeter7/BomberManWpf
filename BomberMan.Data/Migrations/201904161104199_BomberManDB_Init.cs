namespace BomberMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BomberManDB_Init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GameStates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Score = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Maps",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Coordinate = c.String(),
                        Type = c.Int(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.GameStates", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Players",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        Password = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.HighScores",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Score = c.Int(nullable: false),
                        PlayerId = c.Guid(nullable: false),
                        CreatedDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Players", t => t.PlayerId, cascadeDelete: true)
                .Index(t => t.PlayerId);
            
            CreateTable(
                "dbo.PlayerGameStates",
                c => new
                    {
                        Player_Id = c.Guid(nullable: false),
                        GameState_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Player_Id, t.GameState_Id })
                .ForeignKey("dbo.Players", t => t.Player_Id, cascadeDelete: true)
                .ForeignKey("dbo.GameStates", t => t.GameState_Id, cascadeDelete: true)
                .Index(t => t.Player_Id)
                .Index(t => t.GameState_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.HighScores", "PlayerId", "dbo.Players");
            DropForeignKey("dbo.PlayerGameStates", "GameState_Id", "dbo.GameStates");
            DropForeignKey("dbo.PlayerGameStates", "Player_Id", "dbo.Players");
            DropForeignKey("dbo.Maps", "Id", "dbo.GameStates");
            DropIndex("dbo.PlayerGameStates", new[] { "GameState_Id" });
            DropIndex("dbo.PlayerGameStates", new[] { "Player_Id" });
            DropIndex("dbo.HighScores", new[] { "PlayerId" });
            DropIndex("dbo.Maps", new[] { "Id" });
            DropTable("dbo.PlayerGameStates");
            DropTable("dbo.HighScores");
            DropTable("dbo.Players");
            DropTable("dbo.Maps");
            DropTable("dbo.GameStates");
        }
    }
}
