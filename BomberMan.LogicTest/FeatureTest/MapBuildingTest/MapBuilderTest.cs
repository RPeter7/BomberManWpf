using System.Xml.Linq;
using BomberMan.Logic.Feature.MapBuilding;
using BomberMan.Logic.Feature.MapBuilding.Factory;
using BomberMan.Logic.Feature.MapBuilding.Factory.Interfaces;
using BomberMan.Logic.Feature.MapBuilding.Interfaces;
using BomberMan.Logic.Feature.MapBuilding.XmlElementFinding;
using BomberMan.Logic.Feature.MapBuilding.XmlElementFinding.Interfaces;
using BomberMan.Logic.MapElements;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.MapBuildingTest
{
    [TestFixture]
    public class MapBuilderTest
    {
        private IMapElementFactory _mapElementFactory;
        private IXmlElementFinder _xmlElementFinder;
        private IMapBuilder _mapBuilder;
        private XDocument _testDocument;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _testDocument = new XDocument(
                new XElement("coordinates",
                    new XElement("coordinate",
                        new XElement("x", 0),
                        new XElement("y", 0),
                        new XElement("type", "Wall")),
                    new XElement("coordinate",
                        new XElement("x", 0),
                        new XElement("y", 1),
                        new XElement("type", "BreakableWall")),
                    new XElement("coordinate",
                        new XElement("x", 1),
                        new XElement("y", 0),
                        new XElement("type", "Wall"))));
                    
            _mapElementFactory = new MapElementFactory();
            _xmlElementFinder = new XmlElementFinder();
            _mapBuilder = new MapBuilder(_mapElementFactory, _xmlElementFinder);
        }

        [Test]
        public void GivenTestMapXml_WhenBuildMapFromXml_ReturnsMapArray()
        {
            // Act
            var result = _mapBuilder.BuildMapFromXml(_testDocument);

            // Assert
            Check.That(result[0, 0].MapElement).IsInstanceOf<NonBreakableWall>();
            Check.That(result[0, 1].MapElement).IsInstanceOf<BreakableWall>();
            Check.That(result[1, 0].MapElement).IsInstanceOf<NonBreakableWall>();
        }
    }
}
