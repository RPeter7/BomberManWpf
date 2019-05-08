using System.Threading.Tasks;
using System.Xml.Linq;
using BomberMan.Logic.Feature.MapBuilding.Factory.Interfaces;
using BomberMan.Logic.Feature.MapBuilding.Interfaces;
using BomberMan.Logic.Feature.MapBuilding.XmlElementFinding.Interfaces;
using BomberMan.Logic.MapElements.BaseElement;

namespace BomberMan.Logic.Feature.MapBuilding
{
    public class MapBuilder : IMapBuilder
    {
        private readonly IMapElementFactory _mapElementFactory;
        private readonly IXmlElementFinder _xmlElementFinder;

        public MapBuilder(IMapElementFactory mapElementFactory, IXmlElementFinder xmlElementFinder)
        {
            _mapElementFactory = mapElementFactory;
            _xmlElementFinder = xmlElementFinder;
        }

        public MapElementContainer[,] BuildMapFromXml(XDocument mapXml)
        {
            var map = new MapElementContainer[_xmlElementFinder.GetWidth(mapXml) + 1, _xmlElementFinder.GetHigh(mapXml) + 1];

            for (var widthIndex = 0; widthIndex < map.GetLength(0); widthIndex++)
            {
                for (var heightIndex = 0; heightIndex < map.GetLength(1); heightIndex++)
                {
                    var widthIndexToAsync = widthIndex;
                    var heightIndexToAsync = heightIndex;
                    Task.WaitAll(Task.Run(async () =>
                    {
                        var foundedEntityType = await _xmlElementFinder.GetEntityTypesByCoordinatesAsync(mapXml, widthIndexToAsync, heightIndexToAsync);
                        var entityTypeInstance = _mapElementFactory.CreateMapElement(foundedEntityType);
                        map[widthIndexToAsync, heightIndexToAsync] = new MapElementContainer(widthIndexToAsync, heightIndexToAsync, entityTypeInstance);
                    }));
                }
            }

            return map;
        }
    }
}