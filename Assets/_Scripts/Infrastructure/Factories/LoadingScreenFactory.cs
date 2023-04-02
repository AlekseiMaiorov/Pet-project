using Cysharp.Threading.Tasks;
using Infrastructure.AssetManagment;
using Infrastructure.SceneLoad;
using Infrastructure.UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class LoadingScreenFactory : ILoadingScreenFactory
    {
        public LoadingScreen LoadingScreen => (LoadingScreen) _loadingScreen;
        private readonly AddressableProvider _addressableProvider;
        private readonly ISceneLoader _sceneLoader;
        private ILoadingScreen _loadingScreen;

        public LoadingScreenFactory(
            AddressableProvider addressableProvider,
            ISceneLoader sceneLoader)
        {
            _addressableProvider = addressableProvider;
            _sceneLoader = sceneLoader;
        }

        public async UniTask Create<T>() where T : MonoBehaviour, ILoadingScreen
        {
            if (_loadingScreen != null)
            {
                return;
            }

            await _addressableProvider.Load<GameObject>(AssetNames.LOADING_SCREEN);

            var prefab = _addressableProvider.Get<GameObject>(AssetNames.LOADING_SCREEN);
            _loadingScreen = Object.Instantiate(prefab).GetComponent<T>();

            TrackSceneLoaderProgress();
        }

        private void TrackSceneLoaderProgress()
        {
            _loadingScreen.TrackLoadingProgress((ITraceableLoadProgress) _sceneLoader);
        }
    }
}