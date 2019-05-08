using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.MapElements.BaseElement;

namespace BomberMan.Logic.Feature.GameModel
{
    public class GameModel : IGameModel
    {
        private static readonly object MapLock = new object();

        private MapElementContainer[,] _map;
        public MapElementContainer[,] GetMap
        {
            get
            {
                lock (MapLock)
                {
                    return _map;
                }
            }
            set
            {
                lock (MapLock)
                {
                    _map = value;
                }
            }
        }
    }
}