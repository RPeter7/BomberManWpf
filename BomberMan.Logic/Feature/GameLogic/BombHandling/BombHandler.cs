using BomberMan.Logic.Feature.GameLogic.BombHandling.Interfaces;
using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.GameLogic.BombHandling
{
    public class BombHandler : IBombHandler
    {
        private readonly IPositionFinder _positionFinder;
        private readonly IGameModel _gameModel;

        public BombHandler(IPositionFinder positionFinder, IGameModel gameModel)
        {
            _positionFinder = positionFinder;
            _gameModel = gameModel;
        }

        public void DropBomb(PlayerDto player)
        {
            var playerPos = _positionFinder.GetPosition(_gameModel.GetMap, player);
            _gameModel.GetMap[playerPos[0], playerPos[1]].BlowingElement = new Bomb(playerPos[0], playerPos[1], player);
            player.DroppedBomb = (Bomb)_gameModel.GetMap[playerPos[0], playerPos[1]].BlowingElement;
        }

        public void BombBlows(Bomb bomb)
        {
            _gameModel.GetMap[bomb.X, bomb.Y] = new MapElementContainer(bomb.X, bomb.Y, _positionFinder.GetMapElement(_gameModel.GetMap, bomb.X, bomb.Y), new Flame(bomb.X, bomb.Y, bomb.OwnerPlayer));

            if (!(_gameModel.GetMap[bomb.X, bomb.Y + 1].MapElement is NonBreakableWall))
            {
                var mapElement = _positionFinder.GetMapElement(_gameModel.GetMap, bomb.X, bomb.Y + 1);
                if (mapElement is BreakableWall breakableWall)
                {
                    bomb.OwnerPlayer.Hit(breakableWall.Value);
                    mapElement = new EmptyField();
                }
                _gameModel.GetMap[bomb.X, bomb.Y + 1] = new MapElementContainer(bomb.X, bomb.Y + 1, mapElement, new Flame(bomb.X, bomb.Y + 1, bomb.OwnerPlayer));
            }

            if (!(_gameModel.GetMap[bomb.X, bomb.Y - 1].MapElement is NonBreakableWall))
            {
                var mapElement = _positionFinder.GetMapElement(_gameModel.GetMap, bomb.X, bomb.Y - 1);
                if (mapElement is BreakableWall breakableWall)
                {
                    bomb.OwnerPlayer.Hit(breakableWall.Value);
                    mapElement = new EmptyField();
                }
                _gameModel.GetMap[bomb.X, bomb.Y - 1] = new MapElementContainer(bomb.X, bomb.Y - 1, mapElement, new Flame(bomb.X, bomb.Y - 1, bomb.OwnerPlayer));
            }

            if (!(_gameModel.GetMap[bomb.X + 1, bomb.Y].MapElement is NonBreakableWall))
            {
                var mapElement = _positionFinder.GetMapElement(_gameModel.GetMap, bomb.X + 1, bomb.Y);
                if (mapElement is BreakableWall breakableWall)
                {
                    bomb.OwnerPlayer.Hit(breakableWall.Value);
                    mapElement = new EmptyField();
                }
                _gameModel.GetMap[bomb.X + 1, bomb.Y] = new MapElementContainer(bomb.X + 1, bomb.Y, mapElement, new Flame(bomb.X + 1, bomb.Y, bomb.OwnerPlayer));
            }

            if (_gameModel.GetMap[bomb.X - 1, bomb.Y].MapElement is NonBreakableWall) return;
            {
                var mapElement = _positionFinder.GetMapElement(_gameModel.GetMap, bomb.X - 1, bomb.Y);
                if (mapElement is BreakableWall breakableWall)
                {
                    bomb.OwnerPlayer.Hit(breakableWall.Value);
                    mapElement = new EmptyField();
                }
                _gameModel.GetMap[bomb.X - 1, bomb.Y] = new MapElementContainer(bomb.X - 1, bomb.Y, mapElement, new Flame(bomb.X - 1, bomb.Y, bomb.OwnerPlayer));
            }
        }

        public void FlameBlows(Flame flame)
        {
            var mapElementType = _positionFinder.GetMapElement(_gameModel.GetMap, flame.X, flame.Y);

            switch (mapElementType)
            {
                case NonPlayableCharacter npc:
                    npc.IsAlive = false;
                    flame.OwnerPlayer.Hit(npc.Value);
                    break;
                case PlayerDto player:
                    if (player != flame.OwnerPlayer)
                        flame.OwnerPlayer.Hit(player.Value);
                    player.GotHit();
                    break;
            }

            _gameModel.GetMap[flame.X, flame.Y] = new MapElementContainer(flame.X, flame.Y, mapElementType);
        }
    }
}