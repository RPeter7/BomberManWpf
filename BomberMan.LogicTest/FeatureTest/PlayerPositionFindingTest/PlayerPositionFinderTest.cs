using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.PlayerPositionFinding;
using BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;
using BomberMan.LogicTest.TestHelpers;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.PlayerPositionFindingTest
{
    [TestFixture]
    public class PlayerPositionFinderTest
    {
        private MapElementContainer[,] _testMap;
        private IPositionFinder _positionFinder;
        private PlayerDto _testPlayer;

        [OneTimeSetUp]
        public void Setup()
        {
            _testPlayer = new PlayerDto() { Name = "Jani", EntityType = EntityType.PlayerOne };

            _testMap = TestMapGenerator.GenerateMap(_testPlayer);

            _positionFinder = new PositionFinder();
        }

        [Test]
        public void GivenTestMapAndTestPlayer_WhenGetPlayerPosition_ThenReturnsTheCoordinateOfThePlayer()
        {
            // Act
            var result = _positionFinder.GetPosition(_testMap, _testPlayer);

            // Arrange
            Check.That(result).ContainsExactly(1, 1);
        }
    }
}