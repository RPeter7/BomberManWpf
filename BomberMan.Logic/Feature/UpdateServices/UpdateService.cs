using AutoMapper;
using BomberMan.Data.Entities;
using BomberMan.Logic.Feature.UpdateServices.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.Repository.Feature.UnitOfWork;

namespace BomberMan.Logic.Feature.UpdateServices
{
    public class UpdateService<T> : IUpdateService<T> where T : EntityBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Update(T newEntity)
        {
            switch (newEntity)
            {
                case Player player:
                    _unitOfWork.PlayerRepository.Update(player);
                    break;
                //case GameState gameState:
                //    _unitOfWork.GameStateRepository.Update(gameState);
                //    break;
                //case HighScore highScore:
                //    _unitOfWork.HighScoRepository.Update(highScore);
                //    break;
            }
        }

        public bool SaveChanges() => _unitOfWork.Save();
    }
}
