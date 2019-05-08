using System.Timers;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.MapElements.Interfaces
{
    public interface IBlowingElement : IMapElement
    {
        int X { get; set; }

        int Y { get; set; }

        Timer Timer { get; }

        PlayerDto OwnerPlayer { get; set; }
    }
}
