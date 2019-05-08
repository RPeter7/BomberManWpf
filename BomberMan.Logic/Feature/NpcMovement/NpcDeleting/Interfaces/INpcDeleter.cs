using BomberMan.Logic.MapElements;

namespace BomberMan.Logic.Feature.NpcMovement.Interfaces
{
    public interface INpcDeleter
    {
        void DeleteKilledNpc(NonPlayableCharacter npc);
    }
}