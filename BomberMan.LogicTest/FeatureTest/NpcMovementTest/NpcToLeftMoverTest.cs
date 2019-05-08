using BomberMan.Logic.Feature.GameModel.Interfaces;
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
    public class NpcToLeftMoverTest
    {
        private MapElementContainer[,] _testMap;
        private NonPlayableCharacter _testNpc;
        private Mock<IGameModel> _mockedGameModel;
        private INpcToLeftMover _npcToLeftMover;

        [SetUp]
        public void Setup()
        {
            _mockedGameModel = new Mock<IGameModel>();
            _testNpc = new NonPlayableCharacter(MoveDirection.Left);

            _testMap = TestMapGenerator.GenerateMap(_testNpc);

            _mockedGameModel.Setup(x => x.GetMap).Returns(_testMap);

            _npcToLeftMover = new NpcToLeftMover(_mockedGameModel.Object, new PositionFinder(), new NpcTurner());
        }

        [Test]
        public void GivenNpcAndEmptyFieldToLeftOfNpc_WhenMoveNpcLeft_ThenMovesNpcDownInTheMap()
        {
            //Arrange
            _testMap[1, 0] = new MapElementContainer(1, 0, new EmptyField());

            // Act
            _npcToLeftMover.MoveNpcLeft(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<EmptyField>();
            Check.That(_testMap[1, 0].MapElement).IsInstanceOf<NonPlayableCharacter>();
        }

        [Test]
        public void GivenNpcAndWallToLeftOfNpc_WhenMoveNpcLeft_ThenNpcCantMoveDownAndSwitchesDirection()
        {
            //Arrange
            _testMap[1, 0] = new MapElementContainer(1, 0, new BreakableWall());

            // Act
            _npcToLeftMover.MoveNpcLeft(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<NonPlayableCharacter>();
            Check.That(_testMap[1, 0].MapElement).IsInstanceOf<BreakableWall>();
            Check.That(_testNpc.MoveDirection).IsEqualTo(MoveDirection.Right);
        }

        [Test]
        public void GivenNpcAndFlameToLeftOfNpc_WhenMoveNpcLeft_ThenNpcCantMoveDownAndSwitchesDirection()
        {
            //Arrange
            _testMap[1, 0] = new MapElementContainer(1, 0, new EmptyField(), new Flame(1, 0, new PlayerDto()));

            // Act
            _npcToLeftMover.MoveNpcLeft(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<NonPlayableCharacter>();
            Check.That(_testMap[1, 0].BlowingElement).IsInstanceOf<Flame>();
            Check.That(_testNpc.IsAlive).IsEqualTo(false);
        }
    }
}
