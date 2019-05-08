using BomberMan.Logic.Feature.PasswordChecking.Interfaces;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.PasswordChecking
{
    public class PasswordChecker : IPasswordChecker
    {
        public bool IsPasswordCorrect(PlayerDto player, string password) => player.Password.Equals(password);
    }
}
