using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BomberMan.Logic.Feature.Converting.MapElementMapConverting;
using BomberMan.Logic.Feature.GameStateFinding;
using BomberMan.Logic.Model;
using BomberMan.View.Common;
using BomberMan.View.Feature.LoadGame.View;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.View.Feature.LoadGame.ViewModel
{
    public class LoadGameViewModel : BaseViewModel
    {
        private readonly IMapElementMapConverter _mapElementMapConverter;
        private readonly IGameStateFinder _gameStateFinder;

        private PlayerDto _currentPlayer;
        public PlayerDto CurrentPlayer
        {
            get => _currentPlayer;
            set
            {
                Set(ref _currentPlayer, value);
                Task.Run(() => InitPlayersGameStates(value));
            }
        }

        public ObservableCollection<GameStateDto> PlayersGameStates { get; set; } = new ObservableCollection<GameStateDto>();

        public GameStateDto SelectedGameState { get; set; }

        public ICommand CloseWindowCommand { get; set; }
        public ICommand StartGameCommand { get; set; }

        public LoadGameViewModel(IMapElementMapConverter mapElementMapConverter, IGameStateFinder gameStateFinder)
        {
            _mapElementMapConverter = mapElementMapConverter;
            _gameStateFinder = gameStateFinder;
            CloseWindowCommand = new ParameterizedRelayCommand(x => CloseWindow(x as LoadGameWindow));
            StartGameCommand = new RelayCommand(() =>
            {
                var faszkivan = _gameStateFinder.GetGameStateById(SelectedGameState.Id);

                var apad = _mapElementMapConverter.GetMapFromMapElements(SelectedGameState.MapElements,
                    SelectedGameState.Players);
            }, () => SelectedGameState != null);
            Messenger.Default.Register<PlayerDto>(this, x => CurrentPlayer = x);

        }

        private void InitPlayersGameStates(PlayerDto player)
        {
            foreach (var gameState in player.GameStates)
                Application.Current.Dispatcher?.Invoke(() => PlayersGameStates.Add(gameState));
        }

    }
}
