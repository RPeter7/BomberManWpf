using System;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.MapBuilding.Factory;
using BomberMan.Logic.Feature.MapBuilding.Factory.Interfaces;
using BomberMan.Logic.MapElements;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.MapBuildingTest.FactoryTest
{
    [TestFixture]
    public class MapElementFactoryTest
    {
        private IMapElementFactory _mapElementFactory;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapElementFactory = new MapElementFactory();
        }

        [TestCase(EntityType.BreakableWall)]
        public void GivenEntityTypes_WhenCreateMapElement_ThenReturnsAnInstanceOfTheGivenType(EntityType entityType)
        {
            // Act
            var result = _mapElementFactory.CreateMapElement(entityType);

            // Assert
            Check.That(result).IsInstanceOf<BreakableWall>();
        }

        [TestCase(EntityType.PlayerOne)]
        [TestCase(EntityType.PlayerTwo)]
        public void GivenEntityTypeThatFactoryCantBuild_WhenCreateMapElement_ThenThrowsArgumentOutOfRangeException(EntityType entityType)
        {
            // Assert
            Check.ThatCode(() => { _mapElementFactory.CreateMapElement(entityType); }).Throws<ArgumentOutOfRangeException>();
        }
    }
}
