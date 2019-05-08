using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.GameSaving.Interfaces
{
    public interface IGameSaver
    {
        void SaveGame(MapElementContainer[,] map, PlayerDto playerOne, PlayerDto playerTwo);
    }
}
