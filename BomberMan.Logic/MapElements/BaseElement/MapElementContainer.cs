using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.MapElements.BaseElement
{
    public class MapElementContainer
    {
        public int X { get; set; }

        public int Y { get; set; }

        public IMapElement MapElement { get; set; }

        public IBlowingElement BlowingElement { get; set; }

        public MapElementContainer(int x, int y, IMapElement mapElement, IBlowingElement blowingElement = null)
        {
            X = x;
            Y = y;
            MapElement = mapElement;
            BlowingElement = blowingElement;
        }
    }
}