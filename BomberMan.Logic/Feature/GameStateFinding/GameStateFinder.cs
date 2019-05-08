using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BomberMan.Data.Entities;
using BomberMan.Logic.Model;
using BomberMan.Repository.Feature.UnitOfWork;

namespace BomberMan.Logic.Feature.GameStateFinding
{
    public interface IGameStateFinder
    {
        GameStateDto GetGameStateById(Guid gameStateId);
    }

    public class GameStateFinder : IGameStateFinder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GameStateFinder(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public GameStateDto GetGameStateById(Guid gameStateId) => _mapper.Map<GameStateDto>(_unitOfWork.GameStateRepository.GetById(gameStateId));
    }
}
