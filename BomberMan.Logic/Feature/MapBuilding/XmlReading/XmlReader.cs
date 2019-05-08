using System.Xml.Linq;

namespace BomberMan.Logic.Feature.MapBuilding.XmlReading
{
    public static class XmlReader
    {
        public static XDocument Read(string path) => XDocument.Load(path);
    }
}
