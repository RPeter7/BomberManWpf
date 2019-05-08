using System.Windows;
using System.Windows.Input;
using BomberMan.Logic.Model;
using BomberMan.View.Common;
using BomberMan.View.Feature.HighScore.View;
using BomberMan.View.Feature.LoadGame.View;
using BomberMan.View.Feature.PlayerSelecting.View;
using BomberMan.View.Feature.Settings.View;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.View.Feature.Menu.ViewModel
{
    public class MenuViewModel : BaseViewModel
    {
        private PlayerDto _currentPlayer;
        public PlayerDto CurrentPlayer
        {
            get => _currentPlayer;
            set => Set(ref _currentPlayer, value);
        }

        public ICommand CloseAppCommand { get; set; }
        public ICommand NewGameCommand { get; set; }
        public ICommand OpenHighScoreCommand { get; set; }
        public ICommand OpenLoadGameCommand { get; set; }

        public MenuViewModel()
        {
            Messenger.Default.Register<PlayerDto>(this, x => CurrentPlayer = x);
            CloseAppCommand = new RelayCommand(() => Application.Current.Shutdown());
            NewGameCommand = new RelayCommand(SendCurrentPlayerAndOpenPlayerSelector);
            OpenHighScoreCommand = new RelayCommand(OpenHighScoreWindow);
            OpenLoadGameCommand = new RelayCommand(SendCurrentPlayerOpenLoadGameWindow);
        }

        private void SendCurrentPlayerAndOpenPlayerSelector()
        {
            ShowWindow<PlayerSelectorWindow>();
            SendCurrentPlayer();
        }

        private void OpenHighScoreWindow() => ShowWindow<HighScoreWindow>();

        private void SendCurrentPlayerOpenLoadGameWindow()
        {
            ShowWindow<LoadGameWindow>();
            SendCurrentPlayer();
        } 

        private void SendCurrentPlayer() => Messenger.Default.Send(CurrentPlayer);
    }
}
