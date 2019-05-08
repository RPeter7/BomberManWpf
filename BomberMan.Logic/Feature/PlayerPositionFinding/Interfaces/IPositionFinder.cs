using System.Collections.Generic;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.MapElements.Interfaces;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces
{
    public interface IPositionFinder 
    {
        List<int> GetPosition<T>(MapElementContainer[,] map, T searchedObject) where T : IMapElement;

        IBlowingElement GetBlowingElementFromPosition(MapElementContainer[,] map, int x, int y);

        List<int> GetBlowingElementPosition<T>(MapElementContainer[,] map, T blowingElement) where T : IBlowingElement;

        PlayerDto GetAnotherPlayer(MapElementContainer[,] map, PlayerDto player);

        IMapElement GetMapElement(MapElementContainer[,] map, int x, int y);
    }
}