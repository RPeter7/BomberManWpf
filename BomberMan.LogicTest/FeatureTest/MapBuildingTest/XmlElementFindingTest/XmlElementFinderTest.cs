using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.MapBuilding.XmlElementFinding;
using BomberMan.Logic.Feature.MapBuilding.XmlElementFinding.Interfaces;
using BomberMan.Logic.MapElements;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.Feature
{
    [TestFixture]
    public class XmlElementFinderTest
    {
        private IXmlElementFinder _xmlElementFinder;
        private XDocument _testDocument;
        
        [OneTimeSetUp]
        public void Setup()
        {
            _testDocument = new XDocument(
                new XElement("coordinates",
                    new XElement("coordinate", 
                        new XElement("x",0),
                        new XElement("y",0),
                        new XElement("type", "Wall")),
                    new XElement("coordinate",
                        new XElement("x", 0),
                        new XElement("y", 1),
                        new XElement("type", "BreakableWall")),
                    new XElement("coordinate",
                        new XElement("x", 1),
                        new XElement("y", 0),
                        new XElement("type", "Wall")),
                    new XElement("coordinate",
                        new XElement("x", 1),
                        new XElement("y", 1),
                        new XElement("type", "Bomb"))));

            _xmlElementFinder = new XmlElementFinder();
        }

        [Test]
        public void GivenTestDocument_WhenGetMaxHeight_ReturnsOne()
        {
            // Act
            int? result = _xmlElementFinder.GetHigh(_testDocument);

            // Assert
            Check.That(result).HasAValue().Which.IsPositive().And.IsEqualTo(1);
        }

        [Test]
        public void GivenTestDocument_WhenGetMaxWidth_ReturnsOne()
        {
            // Act
            int? result = _xmlElementFinder.GetWidth(_testDocument);

            // Assert
            Check.That(result).HasAValue().Which.IsPositive().And.IsEqualTo(1);
        }

        [Test]
        public async Task GivenOneOneCoordinate_WhenGetEntityTypesByCoordinatesAsync_ReturnsBomb()
        {
            // Act
            var result = await _xmlElementFinder.GetEntityTypesByCoordinatesAsync(_testDocument, 1, 1);

            // Assert
            Check.That(result).IsInstanceOf<EntityType>().And.IsEqualTo(EntityType.Bomb);
        }
    }
}
