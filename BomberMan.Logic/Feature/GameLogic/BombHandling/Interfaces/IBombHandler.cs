using BomberMan.Logic.MapElements;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.GameLogic.BombHandling.Interfaces
{
    public interface IBombHandler
    {
        void DropBomb(PlayerDto player);

        void BombBlows(Bomb bomb);

        void FlameBlows(Flame flame);
    }
}
