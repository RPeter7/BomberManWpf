using System;
using System.Collections.Generic;
using System.Linq;

namespace BomberMan.Logic.Model
{
    public class GameStateDto : EntityBaseDto
    {
        public int Score { get; set; }

        public ICollection<MapElementDto> MapElements { get; set; }

        public virtual ICollection<PlayerDto> Players { get; set; } = new HashSet<PlayerDto>();

        public GameStateDto() { } //AutoMapper needs it.

        public GameStateDto(IEnumerable<MapElementDto> mapElements, PlayerDto playerOne, PlayerDto playerTwo)
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            //Score = playerOne.Score + playerTwo.Score;
            //MapElements = SetGameStateInMapElements(mapElements, this);
            MapElements = new List<MapElementDto>();
            foreach (var mapElement in mapElements)
                MapElements.Add(mapElement);
            //MapElements = mapElements.ToList();
            //Players.Add(playerOne);
            //Players.Add(playerTwo);
        }

        public List<MapElementDto> SetGameStateInMapElements(IEnumerable<MapElementDto> mapElements, GameStateDto gameState)
        {
            var mapElementArray = mapElements as MapElementDto[] ?? mapElements.ToArray();

            foreach (var mapElement in mapElementArray)
            {
                mapElement.GameState = gameState;
                mapElement.GameStateId = gameState.Id;
            }

            return mapElementArray.ToList();
        }
    }
}