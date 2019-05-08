﻿using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.NpcMovement;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.Feature.NpcMovement.NpcDirectionMoving;
using BomberMan.Logic.Feature.NpcMovement.NpcTurning;
using BomberMan.Logic.Feature.PlayerPositionFinding;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;
using BomberMan.LogicTest.TestHelpers;
using Moq;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.NpcMovementTest
{
    [TestFixture]
    public class NpcToDownMoverTest
    {
        private MapElementContainer[,] _testMap;
        private NonPlayableCharacter _testNpc;
        private Mock<IGameModel> _mockedGameModel;
        private INpcToDownMover _npcToDownMover;

        [SetUp]
        public void Setup()
        {
            _mockedGameModel = new Mock<IGameModel>();
            _testNpc = new NonPlayableCharacter(MoveDirection.Down);

            _testMap = TestMapGenerator.GenerateMap(_testNpc);

            _mockedGameModel.Setup(x => x.GetMap).Returns(_testMap);

            _npcToDownMover = new NpcToDownMover(_mockedGameModel.Object, new PositionFinder(), new NpcTurner());
        }
        
        [Test]
        public void GivenNpcAndEmptyFieldUnderNpc_WhenMoveNpcDown_ThenMovesNpcDownInTheMap()
        {
            //Arrange
            _testMap[2,1] = new MapElementContainer(2,1, new EmptyField());

            // Act
            _npcToDownMover.MoveNpcDown(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<EmptyField>();
            Check.That(_testMap[2, 1].MapElement).IsInstanceOf<NonPlayableCharacter>();
        }

        [Test]
        public void GivenNpcAndWallUnderNpc_WhenMoveNpcDown_ThenNpcCantMoveDownAndSwitchesDirection()
        {
            //Arrange
            _testMap[2, 1] = new MapElementContainer(2, 1, new BreakableWall());

            // Act
            _npcToDownMover.MoveNpcDown(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<NonPlayableCharacter>();
            Check.That(_testMap[2, 1].MapElement).IsInstanceOf<BreakableWall>();
            Check.That(_testNpc.MoveDirection).IsEqualTo(MoveDirection.Up);
        }

        [Test]
        public void GivenNpcAndFlameUnderNpc_WhenMoveNpcDown_ThenNpcCantMoveDownAndSwitchesDirection()
        {
            //Arrange
            _testMap[2, 1] = new MapElementContainer(2, 1, new EmptyField(), new Flame(2,0, new PlayerDto()));

            // Act
            _npcToDownMover.MoveNpcDown(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<NonPlayableCharacter>();
            Check.That(_testMap[2, 1].BlowingElement).IsInstanceOf<Flame>();
            Check.That(_testNpc.IsAlive).IsEqualTo(false);
        }
    }
}