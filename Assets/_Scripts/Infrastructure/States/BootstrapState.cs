using Cysharp.Threading.Tasks;
using Infrastructure.Factories;
using Infrastructure.SceneLoad;
using Infrastructure.UI;
using UnityEngine.AddressableAssets;

namespace Infrastructure.States
{
    public class BootstrapState : IState
    {
        private const string INITIAL_SCENE = "InitialScene";
        private readonly ILoadingScreenFactory _loadingScreenFactory;
        private readonly IGameStateMachine _gameStateMachine;
        private readonly ISceneLoader _sceneLoader;

        public BootstrapState(
            IGameStateMachine gameStateMachine,
            ISceneLoader sceneLoader,
            ILoadingScreenFactory loadingScreenFactory)
        {
            _sceneLoader = sceneLoader;
            _loadingScreenFactory = loadingScreenFactory;
            _gameStateMachine = gameStateMachine;
        }

        public async void Enter()
        {
            await InitializeAddressables();
            await CreateLoadingScreen();
            await _sceneLoader.LoadScene(INITIAL_SCENE, EnterLoadAssetsState);
        }

        public void Exit()
        {
        }

        private async UniTask CreateLoadingScreen()
        {
            await _loadingScreenFactory.Create<LoadingScreen>();
        }

        private void EnterLoadAssetsState()
        {
            _gameStateMachine.Enter<LoadAssetsState>();
        }

        private async UniTask InitializeAddressables()
        {
            await Addressables.InitializeAsync();
        }
    }
}