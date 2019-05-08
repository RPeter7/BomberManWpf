using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.MapBuilding.Interfaces;
using BomberMan.Logic.Feature.MapBuilding.XmlReading;
using BomberMan.Logic.Feature.PlayerFinding.Interfaces;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.MapElements.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.View.Common;
using BomberMan.View.Feature.Game.View;
using BomberMan.View.Feature.PlayerSelecting.View;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.View.Feature.PlayerSelecting.ViewModel
{
    public class PlayerSelectorViewModel : BaseViewModel
    {
        private readonly IPlayerFinder _playerFinder;
        private readonly IMapBuilder _mapBuilder;
        private readonly IGameModel _gameModel;

        public ICommand StartGameCommand { get; set; }
        public ICommand CloseCommand { get; set; }

        public ObservableCollection<PlayerDto> Players { get; set; } = new ObservableCollection<PlayerDto>();

        private PlayerDto _playerOne;
        public PlayerDto PlayerOne
        {
            get => _playerOne;
            set
            {
                Set(ref _playerOne, value);
                Task.Run(() => InitializePlayers(value));
            }
        }

        public PlayerDto PlayerTwo { get; set; }

        public PlayerSelectorViewModel(IPlayerFinder playerFinder, IMapBuilder mapBuilder, IGameModel gameModel)
        {
            _playerFinder = playerFinder;
            _mapBuilder = mapBuilder;
            _gameModel = gameModel;
            StartGameCommand = new ParameterizedRelayCommand(RunGameAndSendPlayers, x => PlayerTwo != null);
            CloseCommand = new ParameterizedRelayCommand(x => CloseWindow(x as PlayerSelectorWindow));
            Messenger.Default.Register<PlayerDto>(this, x => PlayerOne = x);
        }

        private void InitializePlayers(PlayerDto currentPlayer)
        {
            foreach (var player in _playerFinder.GetOtherPlayers(currentPlayer))
                Application.Current.Dispatcher.Invoke(() => Players.Add(player));
        }

        private void RunGameAndSendPlayers(object obj)
        {
            PlayerTwo.EntityType = EntityType.PlayerTwo;
            BuildMapAndAddPlayersToMap();
            ShowWindow<GameWindow>();
            Messenger.Default.Send(new List<PlayerDto>() { PlayerOne, PlayerTwo });
            CloseWindow(obj as PlayerSelectorWindow);
        }

        private void BuildMapAndAddPlayersToMap()
        {
            _gameModel.GetMap = _mapBuilder.BuildMapFromXml(XmlReader.Read("../../MapFile/BombermanMap.xml"));
            _gameModel.GetMap[1, 1] = new MapElementContainer(1, 1, PlayerTwo);
            _gameModel.GetMap[11, 15] = new MapElementContainer(11, 15, PlayerOne);
        }
    }
}