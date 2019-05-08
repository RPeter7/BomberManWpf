using System;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.MapBuilding.XmlElementFinding.Interfaces;

namespace BomberMan.Logic.Feature.MapBuilding.XmlElementFinding
{
    public class XmlElementFinder : IXmlElementFinder
    {
        public async Task<EntityType> GetEntityTypesByCoordinatesAsync(XContainer mapXml, int height, int width)
        {
            var type = await FindTypeAsync(mapXml, height, width);

            return (EntityType)Enum.Parse(typeof(EntityType), !string.IsNullOrEmpty(type) ? type : "Default");
        }

        private static async Task<string> FindTypeAsync(XContainer mapXml, int height, int width)
        {
            var type = default(string);

            await Task.Run(() =>
            {
                 type = mapXml.Descendants("coordinate")
                    .Where(x => Convert.ToInt32(x.Element("x")?.Value) == height &&
                                Convert.ToInt32(x.Element("y")?.Value) == width)
                    .Select(x => x.Element("type")?.Value)
                    .FirstOrDefault();
            });

            return type;
        }

        public int GetHigh(XContainer mapXml) => mapXml.Descendants("coordinate").Max(y => Convert.ToInt32(y.Element("y")?.Value));
        
        public int GetWidth(XContainer mapXml) => mapXml.Descendants("coordinate").Max(x => Convert.ToInt32(x.Element("x")?.Value));
    }
}
