using System.Xml.Linq;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.Feature.MapBuilding.Interfaces
{
    public interface IMapBuilder
    {
        MapElementContainer[,] BuildMapFromXml(XDocument mapXml);
    }
}
