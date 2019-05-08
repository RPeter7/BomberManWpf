using System;
using System.Data.Entity;
using BomberMan.Data.Entities;

namespace BomberMan.Data
{
    public class BomberManDBContext : DbContext
    {
        public BomberManDBContext() : base("name=BomberManDBConnString")
        {

        }

        public DbSet<MapElement> Maps { get; set; }
        public DbSet<GameState> GameStates { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<HighScore> HighScores { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           
        }
    }
}