using System.Data.Entity;
using AutoMapper;
using BomberMan.Data;
using BomberMan.Data.Entities;
using BomberMan.Logic.Feature.Converting.ElementToTypeConverting;
using BomberMan.Logic.Feature.Converting.ElementToTypeConverting.Interfaces;
using BomberMan.Logic.Feature.Converting.MapElementMapConverting;
using BomberMan.Logic.Feature.GameLogic;
using BomberMan.Logic.Feature.GameLogic.BombHandling;
using BomberMan.Logic.Feature.GameLogic.BombHandling.Interfaces;
using BomberMan.Logic.Feature.GameLogic.Interfaces;
using BomberMan.Logic.Feature.GameLogic.PlayerHandling.PlayerMoving;
using BomberMan.Logic.Feature.GameLogic.PlayerHandling.PlayerMoving.Interfaces;
using BomberMan.Logic.Feature.GameModel;
using BomberMan.Logic.Feature.GameModel.Interfaces;
using BomberMan.Logic.Feature.GameSaving;
using BomberMan.Logic.Feature.GameSaving.Interfaces;
using BomberMan.Logic.Feature.GameStateFinding;
using BomberMan.Logic.Feature.HighScoreFinding;
using BomberMan.Logic.Feature.HighScoreFinding.Interfaces;
using BomberMan.Logic.Feature.MapBuilding;
using BomberMan.Logic.Feature.MapBuilding.Factory;
using BomberMan.Logic.Feature.MapBuilding.Factory.Interfaces;
using BomberMan.Logic.Feature.MapBuilding.Interfaces;
using BomberMan.Logic.Feature.MapBuilding.XmlElementFinding;
using BomberMan.Logic.Feature.MapBuilding.XmlElementFinding.Interfaces;
using BomberMan.Logic.Feature.NpcMovement;
using BomberMan.Logic.Feature.NpcMovement.Interfaces;
using BomberMan.Logic.Feature.NpcMovement.NpcDeleting;
using BomberMan.Logic.Feature.NpcMovement.NpcDirectionMoving;
using BomberMan.Logic.Feature.NpcMovement.NpcTurning;
using BomberMan.Logic.Feature.PasswordChecking;
using BomberMan.Logic.Feature.PasswordChecking.Interfaces;
using BomberMan.Logic.Feature.PlayerFinding;
using BomberMan.Logic.Feature.PlayerFinding.Interfaces;
using BomberMan.Logic.Feature.PlayerPositionFinding;
using BomberMan.Logic.Feature.PlayerPositionFinding.Interfaces;
using BomberMan.Logic.Feature.UpdateServices;
using BomberMan.Logic.Feature.UpdateServices.Interfaces;
using BomberMan.Logic.Feature.UploadServices;
using BomberMan.Logic.Feature.UploadServices.Interfaces;
using BomberMan.Logic.Model;
using BomberMan.Repository.Feature.Repository;
using BomberMan.Repository.Feature.Repository.Interfaces;
using BomberMan.Repository.Feature.UnitOfWork;
using BomberMan.View.Common.Mapping;
using BomberMan.View.Feature.Game.ViewModel;
using BomberMan.View.Feature.HighScore.ViewModel;
using BomberMan.View.Feature.LoadGame.ViewModel;
using BomberMan.View.Feature.Login.ViewModel;
using BomberMan.View.Feature.Menu.ViewModel;
using BomberMan.View.Feature.PlayerSelecting.ViewModel;
using BomberMan.View.Feature.Settings.ViewModel;
using BomberMan.View.Feature.SignUp.ViewModel;
using Ninject;
using Ninject.Modules;

namespace BomberMan.View.Common.InversionOfControl
{
    public class IocConfiguration : NinjectModule
    {
        public override void Load()
        {
            Bind<IUploadService<PlayerDto>>().To<UploadService<PlayerDto>>().InTransientScope();
            Bind<IUploadService<GameStateDto>>().To<UploadService<GameStateDto>>().InTransientScope();
            Bind<IUploadService<HighScoreDto>>().To<UploadService<HighScoreDto>>().InTransientScope();
            Bind<IPlayerFinder>().To<PlayerFinder>().InSingletonScope();
            Bind<IPasswordChecker>().To<PasswordChecker>().InTransientScope();
            Bind<IMapBuilder>().To<MapBuilder>().InSingletonScope();
            Bind<IMapElementFactory>().To<MapElementFactory>().InSingletonScope();
            Bind<IXmlElementFinder>().To<XmlElementFinder>().InSingletonScope();
            Bind<IPositionFinder>().To<PositionFinder>().InSingletonScope();
            Bind<IElementToTypeConverter>().To<ElementToTypeConverter>().InTransientScope();
            Bind<IGameSaver>().To<GameSaver>().InTransientScope();
            Bind<IUpdateService<Player>>().To<UpdateService<Player>>().InTransientScope();
            Bind<IUpdateService<GameState>> ().To<UpdateService<GameState>>().InTransientScope();
            Bind<IGameStateFinder>().To<GameStateFinder>().InTransientScope();
            Bind<IMapElementMapConverter>().To<MapElementMapConverter>().InTransientScope();
            Bind<IHighScoreFinder>().To<HighScoreFinder>();

            Bind<DbContext>().To<BomberManDBContext>().InSingletonScope();
            Bind<IUnitOfWork>().To<UnitOfWork>().InTransientScope();

            Bind<IRepository<Player>>().To<Repository<Player>>().InTransientScope();
            Bind<IRepository<GameState>>().To<Repository<GameState>>().InTransientScope();
            Bind<IRepository<HighScore>>().To<Repository<HighScore>>().InTransientScope();
            Bind<IRepository<MapElement>>().To<Repository<MapElement>>().InTransientScope();

            Bind<IGameLogic>().To<GameLogic>().InSingletonScope();
            Bind<IBombHandler>().To<BombHandler>().InSingletonScope();
            Bind<IPlayerToDownMover>().To<PlayerToDownMover>().InSingletonScope();
            Bind<IPlayerToUpMover>().To<PlayerToUpMover>().InSingletonScope();
            Bind<IPlayerToLeftMover>().To<PlayerToLeftMover>().InSingletonScope();
            Bind<IPlayerToRightMover>().To<PlayerToRightMover>().InSingletonScope();

            Bind<INpcMover>().To<NpcMover>().InSingletonScope();
            Bind<INpcToRightMover>().To<NpcToRightMover>().InSingletonScope();
            Bind<INpcToDownMover>().To<NpcToDownMover>().InSingletonScope();
            Bind<INpcToLeftMover>().To<NpcToLeftMover>().InSingletonScope();
            Bind<INpcToUpMover>().To<NpcToUpMover>().InSingletonScope();
            Bind<INpcDeleter>().To<NpcDeleter>().InSingletonScope();
            Bind<INpcTurner>().To<NpcTurner>().InSingletonScope();

            Bind<IGameModel>().To<GameModel>().InSingletonScope();

            var mapperConfig = AutoMapperConfiguration.RegisterMappings();
            Bind<MapperConfiguration>().ToConstant(mapperConfig).InSingletonScope();

            Bind<IMapper>().ToMethod(ctx => new Mapper(mapperConfig, type => ctx.Kernel.Get(type)));

            Bind<SignUpViewModel>().ToSelf().InTransientScope();
            Bind<LoginViewModel>().ToSelf().InTransientScope();
            Bind<SettingsViewModel>().ToSelf().InTransientScope();
            Bind<HighScoreViewModel>().ToSelf().InTransientScope();
            Bind<MenuViewModel>().ToSelf().InTransientScope();
            Bind<PlayerSelectorViewModel>().ToSelf().InSingletonScope();
            Bind<GameViewModel>().ToSelf().InTransientScope();
            Bind<LoadGameViewModel>().ToSelf().InTransientScope();

        }
    }
}
