namespace BomberMan.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class BomberManDB_Changed_Highscore : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.HighScores", "PlayerId", "dbo.Players");
            DropIndex("dbo.HighScores", new[] { "PlayerId" });
            AddColumn("dbo.HighScores", "PlayerName", c => c.String());
            DropColumn("dbo.HighScores", "PlayerId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.HighScores", "PlayerId", c => c.Guid(nullable: false));
            DropColumn("dbo.HighScores", "PlayerName");
            CreateIndex("dbo.HighScores", "PlayerId");
            AddForeignKey("dbo.HighScores", "PlayerId", "dbo.Players", "Id", cascadeDelete: true);
        }
    }
}
