using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.PasswordChecking.Interfaces;
using BomberMan.Logic.Feature.PlayerFinding.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.View.Common;
using BomberMan.View.Feature.Login.View;
using BomberMan.View.Feature.Menu.View;
using BomberMan.View.Feature.SignUp.View;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.View.Feature.Login.ViewModel
{
    public class LoginViewModel : BaseViewModel
    {
        private readonly IPlayerFinder _playerFinder;
        private readonly IPasswordChecker _passwordChecker;
        
        public ObservableCollection<PlayerDto> Players { get; set; } = new ObservableCollection<PlayerDto>();
        public PlayerDto SelectedPlayer { get; set; }

        public ICommand LoginCommand { get; set; }
        public ICommand OpenSignUpWindowCommand { get; set; }
        public ICommand CloseAppCommand { get; set; }

        public LoginViewModel(IPlayerFinder playerFinder, IPasswordChecker passwordChecker)
        {
            _playerFinder = playerFinder;
            _passwordChecker = passwordChecker;
            LoginCommand = new ParameterizedRelayCommand(Login, IsLoginEnabled);
            OpenSignUpWindowCommand = new RelayCommand(OpenSignUpWindow);
            CloseAppCommand = new RelayCommand(() => Application.Current.Shutdown());
            InitializePlayers();
            Messenger.Default.Register<NotificationMessage<PlayerDto>>(this, x => Application.Current.Dispatcher.Invoke(() => Players.Add(x.Content)));
        }

        private void InitializePlayers()
        {
            //Application.Current.Dispatcher?.Invoke(() => Players.Clear());
            Task.Run(() =>
            {
                foreach (var player in _playerFinder.GetAllPlayers())
                    Application.Current.Dispatcher.Invoke(() => Players.Add(player));
            });
        }

        private void Login(object obj)
        {
            var loginWindowInstance = obj as LoginWindow;
            var loginResult = _passwordChecker.IsPasswordCorrect(SelectedPlayer, loginWindowInstance?.PW.Password);
            if (loginResult)
            {
                ShowWindow<MenuWindow>();
                CloseWindow(loginWindowInstance);
                SelectedPlayer.EntityType = EntityType.PlayerOne;
                Messenger.Default.Send(SelectedPlayer);
            }
            else
            {
                NotifyUser("Helytelen jelszó!");
            } 
        }

        private bool IsLoginEnabled(object obj) => SelectedPlayer != null && !string.IsNullOrEmpty((obj as LoginWindow)?.PW.Password);

        private void OpenSignUpWindow() => ShowWindow<SignUpWindow>();
    }
}
