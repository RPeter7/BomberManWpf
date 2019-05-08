using System.Threading.Tasks;
using System.Xml.Linq;
using BomberMan.Data.Enums;

namespace BomberMan.Logic.Feature.MapBuilding.XmlElementFinding.Interfaces
{
    public interface IXmlElementFinder
    {
        Task<EntityType> GetEntityTypesByCoordinatesAsync(XContainer mapXml, int height, int width);

        int GetHigh(XContainer mapXml);

        int GetWidth(XContainer mapXml);
    }
}
