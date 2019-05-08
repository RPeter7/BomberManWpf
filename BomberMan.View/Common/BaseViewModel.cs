using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using GalaSoft.MvvmLight;

namespace BomberMan.View.Common
{
    public class BaseViewModel : ViewModelBase
    {
        private readonly object padLock = new object();
        private readonly Timer _timer;

        private bool _isSnackbarActive;
        public bool IsSnackbarActive
        {
            get => _isSnackbarActive;
            set
            {
                if (value)
                    _timer.Start();
                Set(ref _isSnackbarActive, value);
            }
        }

        private string _snackbarContent;
        public string SnackbarContent
        {
            get => _snackbarContent;
            set => Set(ref _snackbarContent, value);
        }

        protected BaseViewModel()
        {
            _timer = new Timer(3000);
            _timer.Elapsed += (x, y) => IsSnackbarActive = false;
        }

        protected void ShowWindow<T>() where T : Window, new()
        {
            var view = new T();
            view.Show();
        }

        protected void CloseWindow<T>(T window) where T : Window, new() => window.Close();

        protected void NotifyUser(string message)
        {
            lock (padLock)
            {
                Task.Run(() =>
                {
                    SnackbarContent = message;
                    IsSnackbarActive = true;
                });
            }
        }
    }
}
