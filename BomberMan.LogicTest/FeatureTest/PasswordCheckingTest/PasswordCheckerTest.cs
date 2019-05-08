using BomberMan.Logic.Feature.PasswordChecking;
using BomberMan.Logic.Feature.PasswordChecking.Interfaces;
using BomberMan.Logic.Model;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.PasswordCheckingTest
{
    [TestFixture]
    public class PasswordCheckerTest
    {
        private IPasswordChecker _passwordChecker;

        [OneTimeSetUp]
        public void Setup()
        {
            _passwordChecker = new PasswordChecker();
        }
        
        [TestCase("1234")]
        public void GivenCorrectPassword_WhenIsPasswordCorrect_ReturnsTrue(string givenPassword)
        {
            // Arrange
            var player = new PlayerDto() { Name = "Jani", Password = "1234"};

            // Act
            var result = _passwordChecker.IsPasswordCorrect(player, givenPassword);

            // Assert
            Assert.True(result);
        }

        [TestCase("asda")]
        public void GivenWrongPassword_WhenIsPasswordCorrect_ReturnsFalse(string givenPassword)
        {
            // Arrange
            var player = new PlayerDto() { Name = "Jani", Password = "1234" };

            // Act
            var result = _passwordChecker.IsPasswordCorrect(player, givenPassword);

            // Assert
            Assert.False(result);
        }
    }
}
