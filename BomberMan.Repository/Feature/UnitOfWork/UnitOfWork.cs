using System;
using System.Data.Entity;
using System.Diagnostics;
using System.Reflection.Emit;
using BomberMan.Data.Entities;
using BomberMan.Repository.Feature.Repository.Interfaces;

namespace BomberMan.Repository.Feature.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Player> PlayerRepository { get; }

        IRepository<GameState> GameStateRepository { get; }

        IRepository<HighScore> HighScoRepository { get; }

        IRepository<MapElement> MapRepository { get;  }

        bool Save();
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private bool _disposed;

        public IRepository<Player> PlayerRepository { get; } 
        public IRepository<GameState> GameStateRepository { get; }
        public IRepository<HighScore> HighScoRepository { get; }
        public IRepository<MapElement> MapRepository { get; }

        public UnitOfWork(DbContext dbContext, 
            IRepository<Player> playerRepository, 
            IRepository<GameState> gameStateRepository, 
            IRepository<HighScore> highScoRepository,
            IRepository<MapElement> mapRepository) //ToDo: Property DI
        {
            _dbContext = dbContext;
            PlayerRepository = playerRepository;
            GameStateRepository = gameStateRepository;
            HighScoRepository = highScoRepository;
            MapRepository = mapRepository;
        }
        
        public bool Save()
        {
            //try
            //{
                _dbContext.SaveChanges();
                return true;
            //}
            //catch (Exception e)
            //{
            //    Debug.WriteLine(e);
            //    return false;
            //}
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
