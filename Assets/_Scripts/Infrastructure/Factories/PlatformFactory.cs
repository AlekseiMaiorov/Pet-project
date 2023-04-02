using Game.Common;
using Game.Platform;
using Game.Player;
using Game.StaticData;
using Infrastructure.AssetManagment;
using UnityEngine;

namespace Infrastructure.Factories
{
    public class PlatformFactory : IPlatformFactory
    {
        public PlatformsMover PlatformsMover => _platformsMover;

        private readonly AddressableProvider _addressableProvider;
        private IPlatformFactory _platformFactoryImplementation;
        private ObjectPoolMono<Platform> _platforms;
        private PlatformsMover _platformsMover;

        public PlatformFactory(AddressableProvider addressableProvider)
        {
            _addressableProvider = addressableProvider;
        }

        public void Create(PlayerMove playerMove)
        {
            var platformMoverPrefab = _addressableProvider.Get<GameObject>(AssetNames.PLATFORM_MOVER_PREFAB);
            var launchPlatformPrefab = _addressableProvider.Get<GameObject>(AssetNames.LAUNCH_PLATFORM_PREFAB);
            var platformPrefab = _addressableProvider.Get<GameObject>(AssetNames.PLATFORM_PREFAB);
            var config = (PlatformsStaticData) _addressableProvider.Get<ScriptableObject>(AssetNames.PLATFORMS_CONFIG);

            var launchPlatform =
                Object.Instantiate(launchPlatformPrefab, config.LaunchPlatformStartPosition, Quaternion.identity);

            var platformMover = Object.Instantiate(platformMoverPrefab, Vector3.zero, Quaternion.identity);

            _platforms = new ObjectPoolMono<Platform>(platformPrefab.GetComponent<Platform>(),
                                                      config.Count,
                                                      platformMover.transform);

            var platformMoverComponent = platformMover.GetComponent<PlatformsMover>();

            platformMoverComponent.Construct(_platforms.Pool, launchPlatform, config.Settings, playerMove.transform);
            platformMoverComponent.Restart();
            _platforms.SetActiveTrueAllElements();

            _platformsMover = platformMoverComponent;
        }
    }
}