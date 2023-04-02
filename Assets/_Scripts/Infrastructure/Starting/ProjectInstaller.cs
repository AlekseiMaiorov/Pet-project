using Game.PauseGame;
using Game.StaticData;
using Infrastructure.AssetManagment;
using Infrastructure.Factories;
using Infrastructure.PersistentProgress;
using Infrastructure.SaveLoad;
using Infrastructure.SceneLoad;
using Infrastructure.States;
using UnityEngine;
using Zenject;

namespace Infrastructure.Starting
{
    [CreateAssetMenu(fileName = "Project", menuName = "Installers/Project")]
    public class ProjectInstaller : ScriptableObjectInstaller
    {
        public LevelStaticData LevelStaticData;

        public override void InstallBindings()
        {
            BindLevelStaticData();
            BindServices();
            BindFactories();
            Container.Bind<PauseManager>().AsSingle();
            Container.BindInterfacesTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<ProjectStarter>().AsSingle();
        }

        private void BindFactories()
        {
            Container.Bind<IPlayerFactory>().To<PlayerFactory>().AsSingle();
            Container.Bind<IPlatformFactory>().To<PlatformFactory>().AsSingle();
            Container.Bind<IUIFactory>().To<UIFactory>().AsSingle();
            Container.Bind<ILoadingScreenFactory>().To<LoadingScreenFactory>().AsSingle();
            Container.Bind<IStatesFactory>().To<StatesFactory>().AsSingle();
        }

        private void BindLevelStaticData()
        {
            Container.Bind<LevelStaticData>().FromInstance(LevelStaticData);
            Container.QueueForInject(LevelStaticData);
        }

        private void BindServices()
        {
            Container.Bind<IPersistantProgress>().To<PersistantProgress>().AsSingle();
            Container.Bind<ISaveLoadService>().To<SaveLoadService>().AsSingle();
            Container.Bind<AddressableProvider>().AsSingle();
            Container.Bind<ISceneLoader>().To<SceneLoader>().AsSingle();
        }
    }
}