using System.Linq;
using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.NpcMovement;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.Feature.NpcMovement.NpcDeleting;
using BomberMan.Logic.Feature.PlayerPositionFinding;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.LogicTest.TestHelpers;
using Moq;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.NpcMovementTest
{
    [TestFixture]
    public class NpcDeleterTest
    {
        private MapElementContainer[,] _testMap;
        private NonPlayableCharacter _testNpc;
        private Mock<IGameModel> _mockedGameModel; 
        private INpcDeleter _npcDeleter;

        [OneTimeSetUp]
        public void Setup()
        {
            _testNpc = new NonPlayableCharacter(MoveDirection.Down);
            _mockedGameModel = new Mock<IGameModel>();

            _testMap = TestMapGenerator.GenerateMap(_testNpc);

            _mockedGameModel.Setup(x => x.GetMap).Returns(_testMap);

            _npcDeleter = new NpcDeleter(_mockedGameModel.Object, new PositionFinder());
        }

        [Test]
        public void WhenDeleteKilledNpc_ThenClearsTheGivenNpcFromMap()
        {
            // Act
            _npcDeleter.DeleteKilledNpc(_testNpc);

            // Assert
            Check.That(_testMap[1, 1].MapElement).IsInstanceOf<EmptyField>();
            Check.That(_testMap.Cast<MapElementContainer>().Select(x => x.MapElement)).Not
                .Contains(typeof(NonPlayableCharacter));
        }
    }
}