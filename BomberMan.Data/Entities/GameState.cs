using System.Collections.Generic;

namespace BomberMan.Data.Entities
{
    public class GameState : EntityBase
    {
        public int Score { get; set; }

        public ICollection<MapElement> MapElements { get; set; }

        public virtual ICollection<Player> Players { get; set; } = new HashSet<Player>();
    }
}