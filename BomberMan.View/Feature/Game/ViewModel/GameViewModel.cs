using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using BomberMan.Data.Enums;
using BomberMan.Logic.Feature.GameLogic.Interfaces;
using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.GameSaving.Interfaces;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.Feature.UploadServices.Interfaces;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.Model;
using BomberMan.View.Common;
using BomberMan.View.Feature.Game.View;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.View.Feature.Game.ViewModel
{
    public class GameViewModel : BaseViewModel
    {
        private readonly CancellationTokenSource _cancellationTokenSource = new CancellationTokenSource();
        public IGameModel GameModel { get; set; }
        public IGameLogic GameLogic { get; set; }
        private readonly INpcMover _npcMover;
        private readonly IGameSaver _gameSaver;
        private readonly IUploadService<HighScoreDto> _uploadService;

        private MediaPlayer _mediaPlayer;

        private PlayerDto _playerOne;
        public PlayerDto PlayerOne
        {
            get => _playerOne;
            set => Set(ref _playerOne, value);
        }

        private PlayerDto _playerTwo;
        public PlayerDto PlayerTwo
        {
            get => _playerTwo;
            set => Set(ref _playerTwo, value);
        }

        public double HelpGridHeight => SystemParameters.PrimaryScreenHeight;

        private bool _isHelpOpened;
        public bool IsHelpOpened
        {
            get => _isHelpOpened;
            set => Set(ref _isHelpOpened, value);
        }

        public ICommand MuteCommand { get; set; }
        public ICommand CloseCommand { get; set; }
        public ICommand OpenCloseHelpCommand { get; set; }

        public ICommand MoveDownPlayerOneCommand { get; set; }
        public ICommand MoveUpPlayerOneCommand { get; set; }
        public ICommand MoveLeftPlayerOneCommand { get; set; }
        public ICommand MoveRightPlayerOneCommand { get; set; }
        public ICommand DropBombPlayerOneCommand { get; set; }

        public ICommand MoveDownPlayerTwoCommand { get; set; }
        public ICommand MoveUpPlayerTwoCommand { get; set; }
        public ICommand MoveLeftPlayerTwoCommand { get; set; }
        public ICommand MoveRightPlayerTwoCommand { get; set; }
        public ICommand DropBombPlayerTwoCommand { get; set; }

        public ICommand SaveGameCommand { get; set; }

        public GameViewModel(IGameModel gameModel, IGameLogic gameLogic, IGameSaver gameSaver, INpcMover npcMover, IUploadService<HighScoreDto> uploadService)
        {
            GameModel = gameModel;
            GameLogic = gameLogic;
            _npcMover = npcMover;
            _gameSaver = gameSaver;
            _uploadService = uploadService;
            InitCommands();
            InitNonPlayableCharacters();
            //PlayMusic();

            Messenger.Default.Register<List<PlayerDto>>(this, ReceivePlayers);
            Messenger.Default.Register<NotificationMessage<PlayerDto>>(this, x => AnotherPlayerWon(x.Content));
            Messenger.Default.Register<NotificationMessage<EntityType>>(this, x =>
            {
                switch (x.Content)
                {
                    case EntityType.PlayerOne:
                        RaisePropertyChanged(nameof(PlayerOne));
                        break;
                    case EntityType.PlayerTwo:
                        RaisePropertyChanged(nameof(PlayerTwo));
                        break;
                }
            });
        }

        private void InitCommands()
        {
            MuteCommand = new RelayCommand(SetMusicMute);
            CloseCommand = new ParameterizedRelayCommand(x =>
            {
                var result = MessageBox.Show("Biztos benne,hogy mentés nélkül lép ki?", "Kilépés mentés nélkül",
                    MessageBoxButton.YesNo, MessageBoxImage.Hand, MessageBoxResult.No);
                if (result == MessageBoxResult.Yes)
                {
                    //_mediaPlayer.Stop();
                    _cancellationTokenSource.Cancel();
                    CloseWindow(x as GameWindow);
                }
            });
            OpenCloseHelpCommand = new RelayCommand(() => IsHelpOpened = !IsHelpOpened);
            MoveDownPlayerOneCommand = new RelayCommand(() => GameLogic.MoveDown(PlayerOne));
            MoveUpPlayerOneCommand = new RelayCommand(() => GameLogic.MoveUp(PlayerOne));
            MoveLeftPlayerOneCommand = new RelayCommand(() => GameLogic.MoveLeft(PlayerOne));
            MoveRightPlayerOneCommand = new RelayCommand(() => GameLogic.MoveRight(PlayerOne));
            DropBombPlayerOneCommand = new RelayCommand(() => GameLogic.DropBomb(PlayerOne));
            MoveDownPlayerTwoCommand = new RelayCommand(() => GameLogic.MoveDown(PlayerTwo));
            MoveUpPlayerTwoCommand = new RelayCommand(() => GameLogic.MoveUp(PlayerTwo));
            MoveLeftPlayerTwoCommand = new RelayCommand(() => GameLogic.MoveLeft(PlayerTwo));
            MoveRightPlayerTwoCommand = new RelayCommand(() => GameLogic.MoveRight(PlayerTwo));
            DropBombPlayerTwoCommand = new RelayCommand(() => GameLogic.DropBomb(PlayerTwo));

            SaveGameCommand = new RelayCommand(() => _gameSaver.SaveGame(GameModel.GetMap, PlayerOne, PlayerTwo));
        }

        private void InitNonPlayableCharacters()
        {
            var npc1 = new NonPlayableCharacter(MoveDirection.Up);
            GameModel.GetMap[11, 1] = new MapElementContainer(11, 1, npc1);

            var npc2 = new NonPlayableCharacter(MoveDirection.Down);
            GameModel.GetMap[1, 15] = new MapElementContainer(1, 15, npc2);

            var npc3 = new NonPlayableCharacter(MoveDirection.Right);
            GameModel.GetMap[3, 7] = new MapElementContainer(3, 7, npc3);

            var npc4 = new NonPlayableCharacter(MoveDirection.Down);
            GameModel.GetMap[5, 7] = new MapElementContainer(5, 7, npc4);

            var tasks = new List<Task>()
            {
                Task.Run(() => _npcMover.MoveNpc(npc1), _cancellationTokenSource.Token),
                Task.Run(() => _npcMover.MoveNpc(npc2), _cancellationTokenSource.Token),
                Task.Run(() => _npcMover.MoveNpc(npc3), _cancellationTokenSource.Token),
                Task.Run(() => _npcMover.MoveNpc(npc4), _cancellationTokenSource.Token)
            };
        }

        private void ReceivePlayers(object players)
        {
            var listOfPlayers = players as List<PlayerDto>;
            PlayerOne = listOfPlayers?.First(x => x.EntityType == EntityType.PlayerOne);
            PlayerTwo = listOfPlayers?.First(x => x.EntityType == EntityType.PlayerTwo);
        }

        private void PlayMusic()
        {
            var path = Environment.CurrentDirectory + "\\Music.wav";
            _mediaPlayer = new MediaPlayer();
            _mediaPlayer.Open(new Uri(path));
            _mediaPlayer.MediaEnded += (x, y) =>
            {
                _mediaPlayer.Position = TimeSpan.Zero;
                _mediaPlayer.Play();
            };
            _mediaPlayer.Play();
        }

        public void AnotherPlayerWon(PlayerDto player)
        {
            _cancellationTokenSource.Cancel();
            var winnerPlayer = player.EntityType == EntityType.PlayerOne ? PlayerTwo : PlayerOne;
            MessageBox.Show($"{ winnerPlayer.Name } nyert! \nPontszáma: { winnerPlayer.Score}");
            _uploadService.Upload(new HighScoreDto(winnerPlayer.Score, winnerPlayer.Name));
            _uploadService.SaveChanges();
        }
        
        //private void SetMusicMute() => _mediaPlayer.Volume = Convert.ToInt32(_mediaPlayer.Volume) != 0 ? 0 : 20;
        private void SetMusicMute()
        {
            if (_mediaPlayer.Volume != 0)
            {
                _mediaPlayer.Volume = 0;
            }
            else _mediaPlayer.Volume = 20;
        }

        public string DateTimeLong => DateTime.Now.ToLongDateString();
    }
}
