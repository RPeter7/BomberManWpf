using BomberMan.Logic.MapElements.BaseElement;

namespace BomberMan.Logic.Feature.GameModel.Interfaces
{
    public interface IGameModel
    {
        MapElementContainer[,] GetMap { get; set; }
    }
}