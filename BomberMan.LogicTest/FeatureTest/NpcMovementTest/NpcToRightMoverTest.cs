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
    public class NpcToRightMoverTest
    {
        private MapElementContainer[,] _testMap;
        private NonPlayableCharacter _testNpc;
        private Mock<IGameModel> _mockedGameModel;
        private INpcToRightMover _npcToRightMover;

        [SetUp]
        public void Setup()
        {
            _mockedGameModel = new Mock<IGameModel>();
            _testNpc = new NonPlayableCharacter(MoveDirection.Right);

            _testMap = TestMapGenerator.GenerateMap(_testNpc);

            _mockedGameModel.Setup(x => x.GetMap).Returns(_testMap);

            _npcToRightMover = new NpcToRightMover(_mockedGameModel.Object, new PositionFinder(), new NpcTurner());
        }

        [Test]
        public void GivenNpcAndEmptyFieldToRightOfNpc_WhenMoveNpcRight_ThenMovesNpcDownInTheMap()
        {
            //Arrange
            _testMap[1, 2] = new MapElementContainer(1, 2, new EmptyField());

            // Act
            _npcToRightMover.MoveNpcRight(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<EmptyField>();
            Check.That(_testMap[1, 2].MapElement).IsInstanceOf<NonPlayableCharacter>();
        }

        [Test]
        public void GivenNpcAndWallToRightOfNpc_WhenMoveNpcRight_ThenNpcCantMoveDownAndSwitchesDirection()
        {
            //Arrange
            _testMap[1, 2] = new MapElementContainer(1, 2, new BreakableWall());

            // Act
            _npcToRightMover.MoveNpcRight(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<NonPlayableCharacter>();
            Check.That(_testMap[1, 2].MapElement).IsInstanceOf<BreakableWall>();
            Check.That(_testNpc.MoveDirection).IsEqualTo(MoveDirection.Left);
        }

        [Test]
        public void GivenNpcAndFlameToRightOfNpc_WhenMoveNpcRight_ThenNpcCantMoveDownAndSwitchesDirection()
        {
            //Arrange
            _testMap[1, 2] = new MapElementContainer(1, 2, new EmptyField(), new Flame(1, 2, new PlayerDto()));

            // Act
            _npcToRightMover.MoveNpcRight(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<NonPlayableCharacter>();
            Check.That(_testMap[1, 2].BlowingElement).IsInstanceOf<Flame>();
            Check.That(_testNpc.IsAlive).IsEqualTo(false);
        }
    }
}
