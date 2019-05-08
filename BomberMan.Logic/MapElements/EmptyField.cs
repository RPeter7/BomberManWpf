using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.MapElements
{
    public class EmptyField : IMapElement
    {
        public string PathOfImage { get; } = string.Empty;
    }
}
