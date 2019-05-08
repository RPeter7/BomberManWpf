using AutoMapper;
using BomberMan.Data.Entities;
using BomberMan.Logic.Feature.Converting.MapElementMapConverting;
using BomberMan.Logic.Feature.GameSaving.Interfaces;
using BomberMan.Logic.Feature.PlayerFinding.Interfaces;
using BomberMan.Logic.Feature.UpdateServices.Interfaces;
using BomberMan.Logic.MapElements.BaseElement;
using BomberMan.Logic.MapElements.Interfaces;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.GameSaving
{
    public class GameSaver : IGameSaver
    {
        private readonly IMapElementMapConverter _mapElementMapConverter;
        private readonly IPlayerFinder _playerFinder;
        private readonly IMapper _mapper;
        private readonly IUpdateService<Player> _updateService;

        public GameSaver(IMapElementMapConverter mapElementMapConverter, IPlayerFinder playerFinder, IMapper mapper, IUpdateService<Player> updateService)
        {
            _mapElementMapConverter = mapElementMapConverter;
            _playerFinder = playerFinder;
            _mapper = mapper;
            _updateService = updateService;
        }

        public void SaveGame(MapElementContainer[,] map, PlayerDto playerOne, PlayerDto playerTwo)
        {
            var gameStateToSave = _mapper.Map<GameState>(new GameStateDto(_mapElementMapConverter.GetMapElementsFromMap(map), playerOne, playerTwo));

            var playerOneFromDb = _playerFinder.GetPlayerById(playerOne.Id);
            var playerTwoFromDb = _playerFinder.GetPlayerById(playerTwo.Id);
         
            playerOneFromDb.GameStates.Add(gameStateToSave);
            playerTwoFromDb.GameStates.Add(gameStateToSave);

            _updateService.Update(playerOneFromDb);
            _updateService.Update(playerTwoFromDb);
            _updateService.SaveChanges();
        }
    }
}