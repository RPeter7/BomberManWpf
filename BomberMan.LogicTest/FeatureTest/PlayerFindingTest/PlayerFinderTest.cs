using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using BomberMan.Data.Entities;
using BomberMan.Logic.Feature.PlayerFinding;
using BomberMan.Logic.Feature.PlayerFinding.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.Repository.Feature.Repository.Interfaces;
using BomberMan.Repository.Feature.UnitOfWork;
using Moq;
using NFluent;
using NUnit.Framework;

namespace BomberMan.LogicTest.FeatureTest.PlayerFindingTest
{
    [TestFixture]
    public class PlayerFinderTest
    {
        private Mock<IRepository<Player>> _mockedRepository;
        private Mock<IUnitOfWork> _mockedUnitOfWork;
        private IMapper _mapper;
        private IEnumerable<Player> _players;
        private IPlayerFinder _playerFinder;

        [OneTimeSetUp]
        public void Setup()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => { cfg.CreateMap<Player, PlayerDto>(); }));
            _mockedRepository = new Mock<IRepository<Player>>();
            _mockedUnitOfWork = new Mock<IUnitOfWork>();

            _players = new List<Player>()
            {
                new Player() {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Name = "Jani",
                    Password = "1234",
                },
                new Player() {
                    Id = Guid.NewGuid(),
                    CreatedDate = DateTime.Now,
                    Name = "Hanka",
                    Password = "asda",
                },
            };

            _mockedRepository.Setup(x => x.GetAll()).Returns(_players.AsEnumerable);
            _mockedRepository.Setup(x => x.GetById(It.IsAny<Guid>())).Returns(_players.First);
            _mockedUnitOfWork.Setup(x => x.PlayerRepository).Returns(_mockedRepository.Object);

            _playerFinder = new PlayerFinder(_mockedUnitOfWork.Object, _mapper);
        }

        [Test]
        public void WhenGetAllComponent_ReturnsAllComponents()
        {
            // Act
            var result = _playerFinder.GetAllPlayers();

            // Assert
            Check.That(result.Extracting("Name")).ContainsExactly("Jani", "Hanka");
        }

        [Test]
        public void GivenExistingPlayerId_WhenGetPlayerById_ThenReturnsTheCorrectPlayer()
        {
            // Act
            var result = _playerFinder.GetPlayerById(_players.First().Id);

            // Assert
            Check.That(result.Name).IsEqualTo("Jani");
        }
    }
}
