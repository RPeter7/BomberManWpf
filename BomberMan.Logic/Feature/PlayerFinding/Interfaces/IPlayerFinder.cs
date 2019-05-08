using System;
using System.Collections.Generic;
using BomberMan.Data.Entities;
using BomberMan.Logic.Model;

namespace BomberMan.Logic.Feature.PlayerFinding.Interfaces
{
    public interface IPlayerFinder
    {
        IEnumerable<PlayerDto> GetAllPlayers();

        IEnumerable<PlayerDto> GetOtherPlayers(PlayerDto player);

        Player GetPlayerById(Guid playerId);
    }
}
