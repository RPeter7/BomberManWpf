using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.NpcMovement.NpcDirectionMoving
{
    public class NpcToRightMover : INpcToRightMover
    {
        private readonly IGameModel _gameModel;
        private readonly IPositionFinder _positionFinder;
        private readonly INpcTurner _npcTurner;

        public NpcToRightMover(IGameModel gameModel, IPositionFinder positionFinder, INpcTurner npcTurner)
        {
            _gameModel = gameModel;
            _positionFinder = positionFinder;
            _npcTurner = npcTurner;
        }

        public bool MoveNpcRight(NonPlayableCharacter npc)
        {
            var npcPosition = _positionFinder.GetPosition(_gameModel.GetMap, npc);
            if (_gameModel.GetMap[npcPosition[0], npcPosition[1] + 1].BlowingElement is Flame)
            {
                npc.IsAlive = false;
                return true;
            }

            switch (_gameModel.GetMap[npcPosition[0], npcPosition[1] + 1].MapElement)
            {
                case EmptyField _:
                    _gameModel.GetMap[npcPosition[0], npcPosition[1] + 1] = new MapElementContainer(npcPosition[0], npcPosition[1] + 1, npc);
                    _gameModel.GetMap[npcPosition[0], npcPosition[1]] = new MapElementContainer(npcPosition[0], npcPosition[1], new EmptyField());
                    return true;
                case PlayerDto player:
                    player.GotHit();
                    npc.IsAlive = false;
                    break;
            }

            _npcTurner.TurnNpc(npc);
            return false;
        }
    }
}