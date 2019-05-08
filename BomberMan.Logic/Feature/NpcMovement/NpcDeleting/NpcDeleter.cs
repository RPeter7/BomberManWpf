using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;

namespace BomberMan.Logic.Feature.NpcMovement.NpcDeleting
{
    public class NpcDeleter : INpcDeleter
    {
        private readonly IGameModel _gameModel;
        private readonly IPositionFinder _positionFinder;

        public NpcDeleter(IGameModel gameModel, IPositionFinder positionFinder)
        {
            _gameModel = gameModel;
            _positionFinder = positionFinder;
        }

        public void DeleteKilledNpc(NonPlayableCharacter npc)
        {
            var npcPosition = _positionFinder.GetPosition(_gameModel.GetMap, npc);
            _gameModel.GetMap[npcPosition[0], npcPosition[1]] = new MapElementContainer(npcPosition[0], npcPosition[1], new EmptyField());
        }
    }
}