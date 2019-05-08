using System;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.MapBuilding.Factory.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.Feature.MapBuilding.Factory
{
    public class MapElementFactory : IMapElementFactory
    {
        public IMapElement CreateMapElement(EntityType elementType)
        {
            switch (elementType)
            {
                case EntityType.BreakableWall:
                    return new BreakableWall();
                case EntityType.Wall:
                    return new NonBreakableWall();
                case EntityType.Default:
                    return new EmptyField();
                default:
                    throw new ArgumentOutOfRangeException(nameof(elementType), elementType, null);
            }
        }
    }
}
