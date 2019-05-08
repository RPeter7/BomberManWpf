using System.Threading.Tasks;
using System.Windows.Input;
using BomberMan.Logic.Feature.UploadServices.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.View.Common;
using BomberMan.View.Feature.SignUp.View;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.View.Feature.SignUp.ViewModel
{
    public class SignUpViewModel : BaseViewModel
    {
        private readonly IUploadService<PlayerDto> _uploadService;

        public PlayerDto NewPlayer { get; set; } = new PlayerDto();

        public ICommand UploadNewPlayerCommand { get; set; }
        public ICommand CloseWindowCommand { get; set; }

        public SignUpViewModel(IUploadService<PlayerDto> uploadService)
        {
            _uploadService = uploadService;
            UploadNewPlayerCommand = new ParameterizedRelayCommand(UploadNewPlayer, IsUploadEnabled);
            CloseWindowCommand = new ParameterizedRelayCommand(x => CloseWindow(x as SignUpWindow));
        }

        private bool IsUploadEnabled(object obj) =>
            !string.IsNullOrEmpty(NewPlayer.Name) &&
            !string.IsNullOrEmpty(NewPlayer.Password);

        private void UploadNewPlayer(object obj)
        {
            var uploadTask = Task.Run(() =>
            {
                _uploadService.Upload(NewPlayer);
                _uploadService.SaveChanges();
            });
            uploadTask.Wait();
            Messenger.Default.Send(new NotificationMessage<PlayerDto>(NewPlayer, "NewUserAdded"));
            CloseWindowCommand.Execute(obj);
        }
    }
}