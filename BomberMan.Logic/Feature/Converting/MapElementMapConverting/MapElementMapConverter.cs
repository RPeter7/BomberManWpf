using System;
using System.Collections.Generic;
using System.Linq;
using BomberMan.Logic.Feature.Converting.ElementToTypeConverting.Interfaces;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.Converting.MapElementMapConverting
{
    public interface IMapElementMapConverter
    {
        IEnumerable<MapElementDto> GetMapElementsFromMap(MapElementContainer[,] map);

        MapElementContainer[,] GetMapFromMapElements(IEnumerable<MapElementDto> mapElements, IEnumerable<PlayerDto> players);
    }

    public class MapElementMapConverter : IMapElementMapConverter
    {
        private readonly IElementToTypeConverter _elementToTypeConverter;

        public MapElementMapConverter(IElementToTypeConverter elementToTypeConverter)
        {
            _elementToTypeConverter = elementToTypeConverter;
        }

        public IEnumerable<MapElementDto> GetMapElementsFromMap(MapElementContainer[,] map)
        {
            var mapToList = map.Cast<MapElementContainer>();
            return mapToList.Select(actualElement => new MapElementDto()
            {
                Id = Guid.NewGuid(),
                CreatedDate = DateTime.Now,
                X = actualElement.X,
                Y = actualElement.Y,
                Type = _elementToTypeConverter.ConvertToEntityType(actualElement.MapElement)
            }).AsEnumerable();
        }

        public MapElementContainer[,] GetMapFromMapElements(IEnumerable<MapElementDto> mapElements, IEnumerable<PlayerDto> players)
        {
            var map = new MapElementContainer[GetWidthFromMapElements(mapElements) + 1, GetHeightFromMapElements(mapElements) + 1];

            return null;
        }

        private static int GetHeightFromMapElements(IEnumerable<MapElementDto> mapElements) => mapElements.Max(mapElement => mapElement.Y);

        private static int GetWidthFromMapElements(IEnumerable<MapElementDto> mapElements) => mapElements.Max(mapElement => mapElement.X);

    }
}
