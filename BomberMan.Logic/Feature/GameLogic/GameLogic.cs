using BomberMan.Logic.Feature.GameLogic.BombHandling.Interfaces;
using BomberMan.Logic.Feature.GameLogic.Interfaces;
using BomberMan.Logic.Feature.GameLogic.PlayerHandling.PlayerMoving.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.Model;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.Logic.Feature.GameLogic
{
    public class GameLogic : IGameLogic
    {
        private readonly IPlayerToLeftMover _playerToLeftMover;
        private readonly IPlayerToRightMover _playerToRightMover;
        private readonly IPlayerToDownMover _playerToDownMover;
        private readonly IPlayerToUpMover _playerToUpMover;
        private readonly IBombHandler _bombHandler;
        
        public GameLogic(IPlayerToLeftMover playerToLeftMover, IPlayerToRightMover playerToRightMover, IPlayerToDownMover playerToDownMover, IPlayerToUpMover playerToUpMover, IBombHandler bombHandler)
        {
            _playerToLeftMover = playerToLeftMover;
            _playerToRightMover = playerToRightMover;
            _playerToDownMover = playerToDownMover;
            _playerToUpMover = playerToUpMover;
            _bombHandler = bombHandler;

            Messenger.Default.Register<NotificationMessage<Bomb>>(this, x => BombBlows(x.Content));
            Messenger.Default.Register<NotificationMessage<Flame>>(this, x => FlameBlows(x.Content));
        }

        public void MoveLeft(PlayerDto player)
        {
            _playerToLeftMover.MoveLeft(player);
            NotifyUiDrawerChangedOnMapHappened();
        }

        public void MoveDown(PlayerDto player)
        {
            _playerToDownMover.MoveDown(player);
            NotifyUiDrawerChangedOnMapHappened(); 
        }

        public void MoveRight(PlayerDto player)
        {
            _playerToRightMover.MoveRight(player);
            NotifyUiDrawerChangedOnMapHappened();
        }

        public void MoveUp(PlayerDto player)
        {
            _playerToUpMover.MoveUp(player);
            NotifyUiDrawerChangedOnMapHappened();
        }

        public void DropBomb(PlayerDto player)
        {
            _bombHandler.DropBomb(player);
            NotifyUiDrawerChangedOnMapHappened();
        }

        public void BombBlows(Bomb bomb)
        {
            _bombHandler.BombBlows(bomb);
            NotifyUiDrawerChangedOnMapHappened();
        }

        public void FlameBlows(Flame flame)
        {
            _bombHandler.FlameBlows(flame);
            NotifyUiDrawerChangedOnMapHappened();
        }
        
        private static void NotifyUiDrawerChangedOnMapHappened() => Messenger.Default.Send(new NotificationMessage<bool>(true, "MoveResult"));
    }
}