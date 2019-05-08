using System.Collections.Generic;
using BomberMan.Data.Enums;

namespace BomberMan.Data.Entities
{
    public class Player : EntityBase
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public EntityType EntityType { get; set; }

        public int Score { get; set; }

        public virtual ICollection<GameState> GameStates { get; set; } = new HashSet<GameState>();
    }
}