using System.Windows;
using BomberMan.View.Common.InversionOfControl;
using BomberMan.View.Common.Mapping;
using BomberMan.View.Feature.Login.View;

namespace BomberMan.View
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AutoMapperConfiguration.RegisterMappings();
            
            IocKernel.Initialize(new IocConfiguration());

            base.OnStartup(e);
            
            Current.MainWindow = IocKernel.Get<LoginWindow>();
            Current.MainWindow?.Show();
        }
    }
}
