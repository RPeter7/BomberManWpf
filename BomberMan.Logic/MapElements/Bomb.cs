using System.Timers;
using BomberMan.Logic.MapElements.Interfaces;
using BomberMan.Logic.Model;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.Logic.MapElements
{
    public class Bomb : IBlowingElement
    {
        public string PathOfImage { get; } = "../../Images/Bomb.png";

        public int X { get; set; }

        public int Y { get; set; }

        public Timer Timer { get; }

        public PlayerDto OwnerPlayer { get; set; }

        public Bomb(int x, int y, PlayerDto player)
        {
            X = x;
            Y = y;
            OwnerPlayer = player;
            Timer = new Timer() { Interval = 3000 };
            Timer.Elapsed += (a, b) =>
            {
                Messenger.Default.Send(new NotificationMessage<Bomb>(this, "BlowResult"));
                Timer.Stop();
            };
            Timer.Start();
        }
    }
}