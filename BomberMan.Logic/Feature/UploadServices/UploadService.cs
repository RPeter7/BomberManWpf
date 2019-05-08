using AutoMapper;
using BomberMan.Data.Entities;
using BomberMan.Logic.Feature.UploadServices.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.Repository.Feature.UnitOfWork;

namespace BomberMan.Logic.Feature.UploadServices
{
    public class UploadService<T> : IUploadService<T> where T : EntityBaseDto
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UploadService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public void Upload(T newEntity)
        {
            switch (newEntity)
            {
                case PlayerDto playerDto:
                    _unitOfWork.PlayerRepository.Insert(_mapper.Map<Player>(playerDto));
                    break;
                case GameStateDto gameStateDto:
                    _unitOfWork.GameStateRepository.Insert(_mapper.Map<GameState>(gameStateDto));
                    break;
                case HighScoreDto highScoreDto:
                    _unitOfWork.HighScoRepository.Insert(_mapper.Map<HighScore>(highScoreDto));
                    break;
            }
        }

        public bool SaveChanges() =>  _unitOfWork.Save();
    }
}
