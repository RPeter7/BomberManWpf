using System;

namespace BomberMan.Logic.Model
{
    public class HighScoreDto : EntityBaseDto
    {
        public int Score { get; set; }

        public string PlayerName { get; set; }

        public HighScoreDto() { } //AutoMapper needs it.
        
        public HighScoreDto(int score, string winnerPlayerName)
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            Score = score;
            PlayerName = winnerPlayerName;
        }
    }
}
