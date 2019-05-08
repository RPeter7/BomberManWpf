using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.LogicTest.TestHelpers
{
    public static class TestMapGenerator
    {
        public static MapElementContainer[,] GenerateMap(IMapElement mapElement = null)
        {
            var testMap = new MapElementContainer[3,3];

            testMap[0, 0] = new MapElementContainer(0, 0, new NonBreakableWall());
            testMap[0, 1] = new MapElementContainer(0, 1, new BreakableWall());
            testMap[0, 2] = new MapElementContainer(0, 2, new NonBreakableWall());
            testMap[1, 0] = new MapElementContainer(1, 0, new BreakableWall());
            testMap[1, 1] = new MapElementContainer(1, 1, mapElement ?? new EmptyField());
            testMap[1, 2] = new MapElementContainer(1, 2, new BreakableWall());
            testMap[2, 0] = new MapElementContainer(2, 0, new NonBreakableWall());
            testMap[2, 1] = new MapElementContainer(2, 1, new BreakableWall());
            testMap[2, 2] = new MapElementContainer(2, 2, new NonBreakableWall());

            return testMap;
        }
    }
}