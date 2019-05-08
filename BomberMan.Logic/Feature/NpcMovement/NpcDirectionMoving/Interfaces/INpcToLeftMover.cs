using BomberMan.Logic.MapElements;

namespace BomberMan.Logic.Feature.NpcMovement.Interfaces
{
    public interface INpcToLeftMover
    {
        bool MoveNpcLeft(NonPlayableCharacter npc);
    }
}
