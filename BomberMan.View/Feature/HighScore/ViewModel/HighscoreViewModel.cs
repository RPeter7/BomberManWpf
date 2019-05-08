using BomberMan.View.Common;
using BomberMan.View.Feature.HighScore.View;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using BomberMan.Logic.Feature.HighScoreFinding.Interfaces;
using BomberMan.Logic.Model;

namespace BomberMan.View.Feature.HighScore.ViewModel
{
    public class HighScoreViewModel : BaseViewModel
    {
        private readonly IHighScoreFinder _highScoreFinder;

        public ICommand CloseCommand { get; set; }

        public ObservableCollection<HighScoreDto> HighScores { get; set; } = new ObservableCollection<HighScoreDto>();

        public HighScoreViewModel(IHighScoreFinder highScoreFinder)
        {
            _highScoreFinder = highScoreFinder;
            CloseCommand = new ParameterizedRelayCommand(x => CloseWindow(x as HighScoreWindow));
            InitHighScores();
        }

        private void InitHighScores()
        {
            foreach (var highScore in _highScoreFinder.GetAllHighScore())
                Application.Current.Dispatcher?.Invoke(() => HighScores.Add(highScore));
        }
    }
}
