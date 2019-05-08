
using BomberMan.Logic.MapElements;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.GameLogic.Interfaces
{
    public interface IGameLogic
    {
        void MoveUp(PlayerDto player);

        void MoveLeft(PlayerDto player);

        void MoveDown(PlayerDto player);

        void MoveRight(PlayerDto player);

        void DropBomb(PlayerDto player);
    }
}
