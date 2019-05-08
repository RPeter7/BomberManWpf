using System.Collections.Generic;
using System.Linq;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.MapElements.Interfaces;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.PlayerPositionFinding
{
    public class PositionFinder : IPositionFinder
    {
        public List<int> GetPosition<T>(MapElementContainer[,] map, T searchedObject) where T : IMapElement
        {
            var result = map.Cast<MapElementContainer>()
                .FirstOrDefault(x => x.MapElement != null && x.MapElement is T objToSearch && objToSearch.Equals(searchedObject));
            return result != null ? new List<int>() { result.X, result.Y } : null;
        }

        public IBlowingElement GetBlowingElementFromPosition(MapElementContainer[,] map, int x, int y) => map[x, y].BlowingElement;

        public List<int> GetBlowingElementPosition<T>(MapElementContainer[,] map, T blowingElement) where T : IBlowingElement
        {
            var result = map.Cast<MapElementContainer>()
                .FirstOrDefault(x => x.MapElement != null && x.BlowingElement != null && x.BlowingElement.Equals(blowingElement));
            return result != null ? new List<int>() { result.X, result.Y } : null;
        }
        
        public PlayerDto GetAnotherPlayer(MapElementContainer[,] map, PlayerDto player) =>
            map.Cast<MapElementContainer>()
                .FirstOrDefault(x => x.MapElement is PlayerDto anotherPlayer 
                                     && anotherPlayer.EntityType == GetInverseEntityType(player.EntityType))?.MapElement as PlayerDto;

        public IMapElement GetMapElement(MapElementContainer[,] map, int x, int y)
        {
            var mapElementType = map[x, y].MapElement;
            return mapElementType is PlayerDto || mapElementType is NonPlayableCharacter || mapElementType is BreakableWall ? map[x, y].MapElement : new EmptyField();
        }

        private static EntityType GetInverseEntityType(EntityType playerEntityType) =>
            playerEntityType == EntityType.PlayerOne ? EntityType.PlayerTwo : EntityType.PlayerOne;
    }
}