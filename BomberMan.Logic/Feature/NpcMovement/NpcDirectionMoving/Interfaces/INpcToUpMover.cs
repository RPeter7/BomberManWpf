using BomberMan.Logic.MapElements;

namespace BomberMan.Logic.Feature.NpcMovement.Interfaces
{
    public interface INpcToUpMover
    {
        bool MoveNpcUp(NonPlayableCharacter npc);
    }
}