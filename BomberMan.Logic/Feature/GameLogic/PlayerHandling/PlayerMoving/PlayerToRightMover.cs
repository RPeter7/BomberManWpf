﻿using BomberMan.Logic.Feature.GameLogic.PlayerHandling.PlayerMoving.Interfaces;
using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.GameLogic.PlayerHandling.PlayerMoving
{
    public class PlayerToRightMover : IPlayerToRightMover
    {
        private readonly IPositionFinder _positionFinder;
        private readonly IGameModel _gameModel;

        public PlayerToRightMover(IPositionFinder positionFinder, IGameModel gameModel)
        {
            _positionFinder = positionFinder;
            _gameModel = gameModel;
        }

        public void MoveRight(PlayerDto player)
        {
            var playerPos = _positionFinder.GetPosition(_gameModel.GetMap, player);
            if (!(_gameModel.GetMap[playerPos[0], playerPos[1] + 1].MapElement is EmptyField)) return;
            _gameModel.GetMap[playerPos[0], playerPos[1] + 1] = new MapElementContainer(playerPos[0], playerPos[1] + 1, player, _positionFinder.GetBlowingElementFromPosition(_gameModel.GetMap, playerPos[0], playerPos[1] + 1));
            _gameModel.GetMap[playerPos[0], playerPos[1]] = new MapElementContainer(playerPos[0], playerPos[1], new EmptyField(), _positionFinder.GetBlowingElementFromPosition(_gameModel.GetMap, playerPos[0], playerPos[1]));
        }
    }
}