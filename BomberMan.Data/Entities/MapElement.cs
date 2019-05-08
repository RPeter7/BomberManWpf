using System;
using BomberMan.Data.Enums;

namespace BomberMan.Data.Entities
{
    public class MapElement : EntityBase
    {
        public int X { get; set; }

        public int Y { get; set; }

        public EntityType Type { get; set; }

        public Guid GameStateId { get; set; }
        public GameState GameState { get; set; }
    }
}
