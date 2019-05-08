using System;
using BomberMan.Data.Enums;

namespace BomberMan.Logic.Model
{
    public class MapElementDto : EntityBaseDto
    {
        public int X { get; set; }

        public int Y { get; set; }

        public EntityType Type { get; set; }

        public Guid GameStateId { get; set; }
        public GameStateDto GameState { get; set; }

        public MapElementDto()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }
    }
}
