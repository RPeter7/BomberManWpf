using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.PasswordChecking.Interfaces
{
    public interface IPasswordChecker
    {
        bool IsPasswordCorrect(PlayerDto player, string password);
    }
}
