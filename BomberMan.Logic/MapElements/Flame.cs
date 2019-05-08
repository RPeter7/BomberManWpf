using System.Timers;
using BomberMan.Logic.Feature.GameLogic.Interfaces;
using BomberMan.Logic.MapElements.Interfaces;
using BomberMan.Logic.Model;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.Logic.MapElements
{
    public class Flame : IBlowingElement
    {
        public int X { get; set; }

        public int Y { get; set; }

        public string PathOfImage { get; } = "../../Images/Flame.png";

        public Timer Timer { get; }

        public PlayerDto OwnerPlayer { get; set; }

        public Flame(int x, int y, PlayerDto player)
        {
            X = x;
            Y = y;
            OwnerPlayer = player;
            Timer = new Timer() { Interval = 500 };
            Timer.Elapsed += (a, b) =>
            {
                Messenger.Default.Send(new NotificationMessage<Flame>(this, "BlowResult"));
                Timer.Stop();
            };
            Timer.Start();
        }
    }
}
