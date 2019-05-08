using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BomberMan.Logic.Feature.HighScoreFinding.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.Repository.Feature.UnitOfWork;

namespace BomberMan.Logic.Feature.HighScoreFinding
{
    public class HighScoreFinder : IHighScoreFinder
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public HighScoreFinder(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public IEnumerable<HighScoreDto> GetAllHighScore() => _mapper.Map<IEnumerable<HighScoreDto>>(_unitOfWork.HighScoRepository.GetAll().OrderByDescending(x => x.Score));
    }
}