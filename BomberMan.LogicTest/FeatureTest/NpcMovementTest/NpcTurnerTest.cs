using BomberMan.Logic.Feature.NpcMovement;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.Feature.NpcMovement.NpcTurning;
using BomberMan.Logic.MapElements;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.NpcMovementTest
{
    [TestFixture]
    public class NpcTurnerTest
    {
        private INpcTurner _npcTurner;

        [OneTimeSetUp]
        public void Setup()
        {            
            _npcTurner = new NpcTurner();
        }

        [Test]
        public void GivenNpcWithDirectionDown_WhenTurnNpc_ThenChangesMoveDirectionToUp()
        {
            // Arrange
            var testNpc = new NonPlayableCharacter(MoveDirection.Down);

            // Act
            _npcTurner.TurnNpc(testNpc);

            // Assert
            Check.That(testNpc.MoveDirection).IsEqualTo(MoveDirection.Up);
        }

        [Test]
        public void GivenNpcWithDirectionRight_WhenTurnNpc_ThenChangesMoveDirectionToLeft()
        {
            // Arrange
            var testNpc = new NonPlayableCharacter(MoveDirection.Right);

            // Act
            _npcTurner.TurnNpc(testNpc);

            // Assert
            Check.That(testNpc.MoveDirection).IsEqualTo(MoveDirection.Left);
        }
    }
}