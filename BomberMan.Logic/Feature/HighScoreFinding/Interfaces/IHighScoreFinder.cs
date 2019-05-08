using System.Collections.Generic;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.HighScoreFinding.Interfaces
{
    public interface IHighScoreFinder
    {
        IEnumerable<HighScoreDto> GetAllHighScore();
    }
}
