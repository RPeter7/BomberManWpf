using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.MapElements
{
    public abstract class Wall : IMapElement
    {
        public abstract string PathOfImage { get; }
    }
}