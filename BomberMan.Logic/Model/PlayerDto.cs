using System;
using System.Collections.Generic;
using BomberMan.Data.Enums;
using BomberMan.Logic.MapElements;
using BomberMan.Logic.MapElements.Interfaces;
using GalaSoft.MvvmLight.Messaging;

namespace BomberMan.Logic.Model
{
    public class PlayerDto : EntityBaseDto, IMapElement, IValuableObject
    {
        public string Name { get; set; }

        public string Password { get; set; }

        public EntityType EntityType { get; set; }

        public int Score { get; set; }

        public virtual ICollection<GameStateDto> GameStates { get; set; } = new HashSet<GameStateDto>();

        public string PathOfImage => EntityType == EntityType.PlayerOne ? "../../Images/PlayerOne.png" : "../../Images/PlayerTwo.png";

        public int Value { get; } = 2000;

        public Bomb DroppedBomb { get; set; }

        public int Lives { get; set; } = 3;

        public PlayerDto()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
        }

        public void GotHit()
        {
            Lives--;
            if (Lives == default)
                Messenger.Default.Send(new NotificationMessage<PlayerDto>(this, "MoveResult"));
            else
                Messenger.Default.Send(new NotificationMessage<EntityType>(EntityType, "HitResult"));
        }

        public void Hit(int hitObjectScore)
        {
            Score += hitObjectScore;
            Messenger.Default.Send(new NotificationMessage<EntityType>(EntityType, "HitResult"));
        }
    }
}