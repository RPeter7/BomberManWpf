using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.Converting.ElementToTypeConverting;
using BomberMan.Logic.Feature.Converting.ElementToTypeConverting.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.Model;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.ConvertingTest.ElementToTypeConvertingTest
{
    [TestFixture]
    public class ElementToTypeConverterTest
    {
        private IElementToTypeConverter _elementToTypeConverter;

        [OneTimeSetUp]
        public void Setup()
        {
            _elementToTypeConverter = new ElementToTypeConverter();
        }
       
        [Test]
        public void GivenMapElement_WhenConvertToEntityType_ThenReturnTheRightType()
        {
            // Arrange
            var nonBreakableWall = new NonBreakableWall();
            var breakableWall = new BreakableWall();
            var playerOne = new PlayerDto() {EntityType = EntityType.PlayerOne};
            var playerTwo = new PlayerDto() { EntityType = EntityType.PlayerTwo };


            // Act
            var typeWall = _elementToTypeConverter.ConvertToEntityType(nonBreakableWall);
            var typeBreakableWall = _elementToTypeConverter.ConvertToEntityType(breakableWall);
            var typePlayerOne = _elementToTypeConverter.ConvertToEntityType(playerOne);
            var typePlayerTwo = _elementToTypeConverter.ConvertToEntityType(playerTwo);


            // Assert
            Check.That(typeWall).IsEqualTo(EntityType.Wall);
            Check.That(typeBreakableWall).IsEqualTo(EntityType.BreakableWall);
            Check.That(typePlayerOne).IsEqualTo(EntityType.PlayerOne);
            Check.That(typePlayerTwo).IsEqualTo(EntityType.PlayerTwo);
        }
    }
}
