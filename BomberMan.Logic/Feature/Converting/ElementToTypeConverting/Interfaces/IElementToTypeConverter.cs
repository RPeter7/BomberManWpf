using BomberMan.Data.Enums;
using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.Feature.Converting.ElementToTypeConverting.Interfaces
{
    public interface IElementToTypeConverter
    {
        EntityType ConvertToEntityType(IMapElement mapElement);
    }
}
