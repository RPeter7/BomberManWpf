using System;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.MapElements;

namespace BomberMan.Logic.Feature.NpcMovement.NpcTurning
{
    public class NpcTurner : INpcTurner
    {
        public void TurnNpc(NonPlayableCharacter npc)
        {
            switch (npc.MoveDirection)
            {
                case MoveDirection.Up:
                    npc.MoveDirection = MoveDirection.Down;
                    break;
                case MoveDirection.Down:
                    npc.MoveDirection = MoveDirection.Up;
                    break;
                case MoveDirection.Right:
                    npc.MoveDirection = MoveDirection.Left;
                    break;
                case MoveDirection.Left:
                    npc.MoveDirection = MoveDirection.Right;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
