using BomberMan.Logic.MapElements;

namespace BomberMan.Logic.Feature.NpcMovement.Interfaces
{
    public interface INpcToRightMover
    {
        bool MoveNpcRight(NonPlayableCharacter npc);
    }
}