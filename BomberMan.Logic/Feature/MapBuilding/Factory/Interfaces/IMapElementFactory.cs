using BomberMan.Data.Enums;
using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.Feature.MapBuilding.Factory.Interfaces
{
    public interface IMapElementFactory
    {
        IMapElement CreateMapElement(EntityType elementType);
    }
}
