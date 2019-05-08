using System;

namespace BomberMan.Data.Entities
{
    public class HighScore : EntityBase
    {
        public int Score { get; set; }

        public string PlayerName { get; set; }
    }
}