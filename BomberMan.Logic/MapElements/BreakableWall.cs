using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.MapElements
{
    public class BreakableWall : Wall, IValuableObject
    {
        public override string PathOfImage { get; } = "../../Images/BreakableWall.png";

        public int Value { get; } = 500;
    }
}
