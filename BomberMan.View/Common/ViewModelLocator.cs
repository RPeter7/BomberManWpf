using BomberMan.View.Common.InversionOfControl;
using BomberMan.View.Feature.Game.ViewModel;
using BomberMan.View.Feature.HighScore.ViewModel;
using BomberMan.View.Feature.LoadGame.ViewModel;
using BomberMan.View.Feature.Login.ViewModel;
using BomberMan.View.Feature.Menu.ViewModel;
using BomberMan.View.Feature.PlayerSelecting.ViewModel;
using BomberMan.View.Feature.Settings.ViewModel;
using BomberMan.View.Feature.SignUp.ViewModel;

namespace BomberMan.View.Common
{
    public class ViewModelLocator
    {
        public SignUpViewModel SignUpViewModel => IocKernel.Get<SignUpViewModel>();

        public LoginViewModel LoginViewModel => IocKernel.Get<LoginViewModel>();

        public SettingsViewModel SettingsViewModel => IocKernel.Get<SettingsViewModel>();

        public HighScoreViewModel HighScoreViewModel => IocKernel.Get<HighScoreViewModel>();

        public MenuViewModel MenuViewModel => IocKernel.Get<MenuViewModel>();

        public PlayerSelectorViewModel PlayerSelectorViewModel => IocKernel.Get<PlayerSelectorViewModel>();

        public GameViewModel GameViewModel => IocKernel.Get<GameViewModel>();

        public LoadGameViewModel LoadGameViewModel => IocKernel.Get<LoadGameViewModel>();
    }
}
