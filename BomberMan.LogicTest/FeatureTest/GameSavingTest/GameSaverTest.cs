using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.GameSaving.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.GameSavingTest
{
    [TestFixture]
    public class GameSaverTest
    {
        private MapElementContainer[,] _testMap;
        private IGameSaver _gameSaver;
        private PlayerDto _testPlayer;

        [OneTimeSetUp]
        public void Setup()
        {
            _testPlayer = new PlayerDto() { Name = "Jani", EntityType = EntityType.PlayerOne };

            _testMap = new MapElementContainer[3, 3];
            _testMap[0, 0] = new MapElementContainer(0, 0, new NonBreakableWall());
            _testMap[0, 1] = new MapElementContainer(0, 1, new BreakableWall());
            _testMap[0, 2] = new MapElementContainer(0, 2, new NonBreakableWall());
            _testMap[1, 0] = new MapElementContainer(1, 0, _testPlayer);
            _testMap[1, 1] = new MapElementContainer(1, 1, new EmptyField());
            _testMap[1, 2] = new MapElementContainer(1, 2, new BreakableWall());
            _testMap[2, 0] = new MapElementContainer(2, 0, new NonBreakableWall());
            _testMap[2, 1] = new MapElementContainer(2, 1, new BreakableWall());
            _testMap[2, 2] = new MapElementContainer(2, 2, new NonBreakableWall());

           // _gameSaver = new GameSaver(new ElementToTypeConverter());
        }


        //[Test]
        //public void GivenMap_WhenGetGameStatesFromMap_ThenReturnsTheListOfGameStates()
        //{
        //    // Act
        //    var result = _gameSaver.GetMapElementsFromMap(_testMap);

        //    // Assert
        //    Check.That(result).CountIs(9);
        //    Check.That(result).ContainsNoNull();
        //    Check.That(result.ElementAt(4).Type).IsInstanceOf<EntityType>();
        //    Check.That(result.ElementAt(3).Type).IsEqualTo(EntityType.PlayerOne);
        //}

        //[Test]
        //public void GivenMapAndPlayers_WhenSaveGame_ThenAddGameStateToPlayersThenSave()
        //{

        //}
    }
}
