using System;
using BomberMan.Logic.MapElements.Interfaces;

namespace BomberMan.Logic.MapElements
{
    public enum MoveDirection
    {
        Up, Down, Right, Left
    }

    public class NonPlayableCharacter : IMapElement, IValuableObject
    {
        private static readonly Random Rnd = new Random();

        public string PathOfImage { get; } = "../../Images/NonPlayer.png";

        public int Value { get; } = 1500;

        public bool IsAlive { get; set; } = true;

        public int Speed { get; set; } = Rnd.Next(500, 1500);

        public MoveDirection MoveDirection { get; set; }

        public NonPlayableCharacter(MoveDirection moveDirection)
        {
            MoveDirection = moveDirection;
        }
    }
}
