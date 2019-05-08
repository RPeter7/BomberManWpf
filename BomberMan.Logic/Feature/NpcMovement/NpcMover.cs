using System;
using System.Threading;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.MapElements;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.Logic.Feature.NpcMovement
{
    public class NpcMover : INpcMover
    {
        private readonly INpcToRightMover _npcToRightMover;
        private readonly INpcToLeftMover _npcToLeftMover;
        private readonly INpcToUpMover _npcToUpMover;
        private readonly INpcToDownMover _npcToDownMover;
        private readonly INpcDeleter _npcDeleter;

        public NpcMover(INpcToRightMover npcToRightMover, INpcToLeftMover npcToLeftMover, INpcToUpMover npcToUpMover, INpcToDownMover npcToDownMover, INpcDeleter npcDeleter)
        {
            _npcToRightMover = npcToRightMover;
            _npcToLeftMover = npcToLeftMover;
            _npcToUpMover = npcToUpMover;
            _npcToDownMover = npcToDownMover;
            _npcDeleter = npcDeleter;
        }

        public void MoveNpc(NonPlayableCharacter npc)
        {
            while (npc.IsAlive)
            {
                Thread.Sleep(npc.Speed);
                switch (npc.MoveDirection)
                {
                    case MoveDirection.Up:
                        if (!_npcToUpMover.MoveNpcUp(npc))
                        {
                            if (_npcToLeftMover.MoveNpcLeft(npc))
                                npc.MoveDirection = MoveDirection.Left;
                            else
                                npc.MoveDirection = _npcToRightMover.MoveNpcRight(npc) ? MoveDirection.Right : MoveDirection.Down;
                        }
                        break;
                    case MoveDirection.Down:
                        if (!_npcToDownMover.MoveNpcDown(npc))
                        {
                            if (_npcToRightMover.MoveNpcRight(npc))
                                npc.MoveDirection = MoveDirection.Right;
                            else
                                npc.MoveDirection =_npcToUpMover.MoveNpcUp(npc) ? MoveDirection.Up : MoveDirection.Left;
                        }
                        break;
                    case MoveDirection.Right:
                        if (!_npcToRightMover.MoveNpcRight(npc))
                        {
                            if (_npcToDownMover.MoveNpcDown(npc))
                                npc.MoveDirection = MoveDirection.Down;
                            else
                                npc.MoveDirection = _npcToUpMover.MoveNpcUp(npc) ? MoveDirection.Up : MoveDirection.Left;
                        }
                        break;
                    case MoveDirection.Left:
                        if (!_npcToLeftMover.MoveNpcLeft(npc))
                        {
                            if (_npcToUpMover.MoveNpcUp(npc))
                                npc.MoveDirection = MoveDirection.Up;
                            else
                                npc.MoveDirection = _npcToDownMover.MoveNpcDown(npc) ? MoveDirection.Down : MoveDirection.Right;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                NotifyUiDrawerMoveHappened();
            }
            _npcDeleter.DeleteKilledNpc(npc);
            NotifyUiDrawerMoveHappened();
        }
        
        private static void NotifyUiDrawerMoveHappened() => Messenger.Default.Send(new NotificationMessage<bool>(true, "MoveResult"));
    }
}