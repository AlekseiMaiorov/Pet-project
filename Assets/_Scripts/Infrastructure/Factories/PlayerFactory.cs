using Game.Common;
using Game.Player;
using Game.StaticData;
using Infrastructure.AssetManagment;
using Infrastructure.SaveLoad;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Infrastructure.Factories
{
    internal class PlayerFactory : IPlayerFactory
    {
        public GameObject Player => _player;
        private readonly AddressableProvider _addressableProvider;
        private readonly IGameStateMachine _gameStateMachine;
        private GameObject _player;
        private ISaveLoadService _saveLoadService;

        public PlayerFactory(
            IGameStateMachine gameStateMachine,
            AddressableProvider addressableProvider,
            ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _addressableProvider = addressableProvider;
        }

        public GameObject Create()
        {
            var prefab = _addressableProvider.Get<GameObject>(AssetNames.PLAYER_PREFAB);
            var config = (PlayerStaticData) _addressableProvider.Get<ScriptableObject>(AssetNames.PLAYER_CONFIG);

            var player = Object.Instantiate(prefab, config.SpawnPoint, Quaternion.identity);

            var collisionObserver = player.GetComponent<CollisionObserver>();
            var triggerObserver = player.GetComponent<TriggerObserver>();
            var rigidbody = player.GetComponent<Rigidbody>();
            var playerMove = player.GetComponent<PlayerMove>();
            var playerSound = player.GetComponent<PlayerSound>();
            var playerDeath = player.GetComponent<PlayerDeath>();
            var playerScore = player.GetComponent<PlayerScore>();
            var playerInput = player.GetComponent<PlayerInput>();

            playerMove.Construct(collisionObserver, rigidbody, config.Settings, playerInput);
            playerSound.Construct(config.SoundClips, playerMove, collisionObserver, playerDeath);
            playerDeath.Construct(config.SpawnPoint, rigidbody, _gameStateMachine);
            playerScore.Construct(triggerObserver);

            _saveLoadService.OnLoadData += playerScore.Score.LoadData;
            _saveLoadService.OnSaveData += playerScore.Score.SaveData;

            _player = player;
            return player;
        }
    }
}