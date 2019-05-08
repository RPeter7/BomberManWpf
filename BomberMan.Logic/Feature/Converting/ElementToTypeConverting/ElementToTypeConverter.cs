using System;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.Converting.ElementToTypeConverting.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.Interfaces;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.Converting.ElementToTypeConverting
{
    public class ElementToTypeConverter : IElementToTypeConverter
    {
        public EntityType ConvertToEntityType(IMapElement mapElement)
        {
            switch (mapElement)
            {
                case Bomb bomb:
                    return EntityType.Bomb;
                case BreakableWall breakableWall:
                    return EntityType.BreakableWall;
                case EmptyField emptyField:
                    return EntityType.Default;
                case NonBreakableWall nonBreakableWall:
                    return EntityType.Wall;
                case PlayerDto playerDto:
                    return WhichPlayerIsGiven(playerDto);
                default:
                    throw new InvalidCastException();
            }
        }

        private static EntityType WhichPlayerIsGiven(PlayerDto player) =>
            player.EntityType == EntityType.PlayerOne
                ? EntityType.PlayerOne
                : EntityType.PlayerTwo;
    }
}
