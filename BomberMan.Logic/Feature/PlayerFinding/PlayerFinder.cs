using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BomberMan.Data.Entities;
using BomberMan.Logic.Feature.PlayerFinding.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.Repository.Feature.UnitOfWork;

namespace BomberMan.Logic.Feature.PlayerFinding
{
    public class PlayerFinder : IPlayerFinder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlayerFinder(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<PlayerDto> GetAllPlayers()
        {
            var allPlayers = _unitOfWork.PlayerRepository.GetAll();
            return _mapper.Map<IEnumerable<PlayerDto>>(allPlayers.ToList());
        }

        public IEnumerable<PlayerDto> GetOtherPlayers(PlayerDto player)
        {
            var allPlayersExceptGiven = _unitOfWork.PlayerRepository.GetAll().Where(x => !x.Name.Equals(player.Name));
            return _mapper.Map<IEnumerable<PlayerDto>>(allPlayersExceptGiven);
        }

        public Player GetPlayerById(Guid playerId) => _unitOfWork.PlayerRepository.GetById(playerId);
    }
}