using AutoMapper;
using BomberMan.Data.Entities;
using BomberMan.Logic.Model;

namespace BomberMan.View.Common.Mapping
{
    public static class AutoMapperConfiguration
    {
        public static MapperConfiguration RegisterMappings()
        {
            return new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<EntityBase, EntityBaseDto>();
                cfg.CreateMap<EntityBaseDto, EntityBase>();
                cfg.CreateMap<GameState, GameStateDto>();
                cfg.CreateMap<GameStateDto, GameState>();
                cfg.CreateMap<HighScore, HighScoreDto>();
                cfg.CreateMap<HighScoreDto, HighScore>();
                cfg.CreateMap<MapElement, MapElementDto>();
                cfg.CreateMap<MapElementDto, MapElement>();
                cfg.CreateMap<Player, PlayerDto>();
                cfg.CreateMap<PlayerDto, Player>();
            });
        }
    }
}
