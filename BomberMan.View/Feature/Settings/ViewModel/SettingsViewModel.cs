using BomberMan.View.Common;
using BomberMan.View.Feature.Settings.View;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BomberMan.View.Feature.Settings.ViewModel;
using System.Windows;
using System.Windows.Input;

namespace BomberMan.View.Feature.Settings.ViewModel
{
    public class SettingsViewModel : BaseViewModel
    {
        public ICommand CloseCommand { get; set; }

        public SettingsViewModel()
        {
            CloseCommand = new ParameterizedRelayCommand(x => CloseWindow(x as SettingsWindow));
        }
    }
}
