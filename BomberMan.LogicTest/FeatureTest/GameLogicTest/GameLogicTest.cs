using System.Net.NetworkInformation;
using BomberMan.Logic.Feature.GameLogic;
using BomberMan.Logic.Feature.GameLogic.BombHandling;
using BomberMan.Logic.Feature.GameLogic.BombHandling.Interfaces;
using BomberMan.Logic.Feature.GameLogic.Interfaces;
using BomberMan.Logic.Feature.GameLogic.PlayerHandling.PlayerMoving;
using BomberMan.Logic.Feature.GameLogic.PlayerHandling.PlayerMoving.Interfaces;
using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.PlayerPositionFinding;
using BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;
using BomberMan.LogicTest.TestHelpers;
using Moq;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.GameLogicTest
{
    [TestFixture]
    public class GameLogicTest
    {
        private Mock<IGameModel> _mockedGameModel;
        private IPlayerToLeftMover _playerToLeftMover;
        private IPlayerToRightMover _playerToRightMover;
        private IPlayerToUpMover _playerToUpMover;
        private IPlayerToDownMover _playerToDownMover;
        private IBombHandler _bombHandler;
        private IPositionFinder _positionFinder;

        private MapElementContainer[,] _testMap;
        private IGameLogic _gameLogic;
        private PlayerDto _testPlayer;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            _testPlayer = new PlayerDto() { Name = "Jani" };
            _positionFinder = new PositionFinder();
        }

        [SetUp]
        public void Setup()
        {
            _mockedGameModel = new Mock<IGameModel>();

            _testMap = TestMapGenerator.GenerateMap(_testPlayer);

            _mockedGameModel.Setup(x => x.GetMap).Returns(_testMap);

            _playerToDownMover = new PlayerToDownMover(_positionFinder, _mockedGameModel.Object);
            _playerToUpMover = new PlayerToUpMover(_positionFinder, _mockedGameModel.Object);
            _playerToRightMover = new PlayerToRightMover(_positionFinder, _mockedGameModel.Object);
            _playerToLeftMover = new PlayerToLeftMover(_positionFinder, _mockedGameModel.Object);
            _bombHandler = new BombHandler(_positionFinder, _mockedGameModel.Object);

            _gameLogic = new GameLogic(_playerToLeftMover, _playerToRightMover, _playerToDownMover, _playerToUpMover, _bombHandler);
        }

        [Test]
        public void GivenActualPlayer_WhenMoveDown_ThenModifiesTheGivenMapStatus()
        {
            // Arrange
            _testMap[0, 1] = new MapElementContainer(0, 1, new EmptyField());

            // Act
            _gameLogic.MoveDown(_testPlayer);

            // Assert
            Check.That(_testMap[0, 1].MapElement).IsInstanceOf<PlayerDto>();
            Check.That(_testMap[0, 1].X).IsEqualTo(0);
            Check.That(_testMap[0, 1].Y).IsEqualTo(1);
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<EmptyField>();
            Check.That(_testMap[1, 1].X).IsEqualTo(1);
            Check.That(_testMap[1, 1].Y).IsEqualTo(1);
        }

        [Test]
        public void GivenActualPlayer_WhenMoveRight_ThenModifiesTheGivenMapStatus()
        {
            // Arrange
            _testMap[1, 2] = new MapElementContainer(1, 2, new EmptyField());

            // Act
            _gameLogic.MoveRight(_testPlayer);

            // Assert
            Check.That(_testMap[1, 2].MapElement).IsInstanceOf<PlayerDto>();
            Check.That(_testMap[1, 2].X).IsEqualTo(1);
            Check.That(_testMap[1, 2].Y).IsEqualTo(2);
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<EmptyField>();
            Check.That(_testMap[1, 1].X).IsEqualTo(1);
            Check.That(_testMap[1, 1].Y).IsEqualTo(1);
        }

        [Test]
        public void GivenActualPlayer_WhenMoveLeft_ThenModifiesTheGivenMapStatus()
        {
            // Arrange 
            _testMap[1,0] = new MapElementContainer(1,0,new EmptyField());

            // Act
            _gameLogic.MoveLeft(_testPlayer);

            // Assert
            Check.That(_testMap[1, 0].MapElement).IsInstanceOf<PlayerDto>();
            Check.That(_testMap[1, 0].X).IsEqualTo(1);
            Check.That(_testMap[1, 0].Y).IsEqualTo(0);
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<EmptyField>();
            Check.That(_testMap[1, 1].X).IsEqualTo(1);
            Check.That(_testMap[1, 1].Y).IsEqualTo(1);
        }

        [Test]
        public void GivenActualPlayer_WhenMoveUp_ThenModifiesTheGivenMapStatus()
        {
            // Arrange
            _testMap[2, 1] = new MapElementContainer(2, 1, new EmptyField());

            // Act
            _gameLogic.MoveUp(_testPlayer);

            // Assert
            Check.That(_testMap[2, 1].MapElement).IsInstanceOf<PlayerDto>();
            Check.That(_testMap[2, 1].X).IsEqualTo(2);
            Check.That(_testMap[2, 1].Y).IsEqualTo(1);
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<EmptyField>();
            Check.That(_testMap[1, 1].X).IsEqualTo(1);
            Check.That(_testMap[1, 1].Y).IsEqualTo(1);
        }

        [Test]
        public void GivenActualPlayerAndNoEmptyFieldUpper_WhenMoveUp_ThenPlayerCantMoveUp()
        {
            // Arrange
            _testMap[2, 1] = new MapElementContainer(2, 1, new BreakableWall());

            // Act
            _gameLogic.MoveUp(_testPlayer);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<PlayerDto>();
            Check.That(_testMap[1, 1].X).IsEqualTo(1);
            Check.That(_testMap[1, 1].Y).IsEqualTo(1);
        }


        [Test]
        public void GivenActualPlayerAndNoEmptyFieldDowner_WhenMoveDown_ThenPlayerCantMoveDown()
        {
            // Arrange
            _testMap[0, 1] = new MapElementContainer(0, 1, new BreakableWall());

            // Act
            _gameLogic.MoveDown(_testPlayer);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<PlayerDto>();
            Check.That(_testMap[1, 1].X).IsEqualTo(1);
            Check.That(_testMap[1, 1].Y).IsEqualTo(1);
        }

        [Test]
        public void GivenActualPlayerAndNoEmptyFieldToRight_WhenMoveRight_ThenPlayerCantMoveRight()
        {
            // Arrange
            _testMap[1, 2] = new MapElementContainer(1, 2, new BreakableWall());

            // Act
            _gameLogic.MoveRight(_testPlayer);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<PlayerDto>();
            Check.That(_testMap[1, 1].X).IsEqualTo(1);
            Check.That(_testMap[1, 1].Y).IsEqualTo(1);
        }

        [Test]
        public void GivenActualPlayerAndNoEmptyFieldLefter_WhenMoveLeft_ThenPlayerCantMoveLeft()
        {
            // Arrange
            _testMap[1, 0] = new MapElementContainer(1, 0, new BreakableWall());

            // Act
            _gameLogic.MoveLeft(_testPlayer);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<PlayerDto>();
            Check.That(_testMap[1, 1].X).IsEqualTo(1);
            Check.That(_testMap[1, 1].Y).IsEqualTo(1);
        }
    }
}