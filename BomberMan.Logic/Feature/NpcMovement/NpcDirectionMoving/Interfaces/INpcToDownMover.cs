using BomberMan.Logic.MapElements;

namespace BomberMan.Logic.Feature.NpcMovement.Interfaces
{
    public interface INpcToDownMover
    {
        bool MoveNpcDown(NonPlayableCharacter npc);
    }
}
