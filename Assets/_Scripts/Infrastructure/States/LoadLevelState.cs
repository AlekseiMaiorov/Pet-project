using Game.Common;
using Game.Player;
using Infrastructure.Factories;
using Infrastructure.SaveLoad;
using Infrastructure.SceneLoad;
using UnityEngine;

namespace Infrastructure.States
{
    public class LoadLevelState : IPayloadState<string>
    {
        private IGameStateMachine _gameStateMachine;
        private IPlatformFactory _platformFactory;
        private IPlayerFactory _playerFactory;
        private ISaveLoadService _saveLoadService;
        private ISceneLoader _sceneLoader;
        private IUIFactory _uiFactory;

        public LoadLevelState(
            IGameStateMachine gameStateMachine,
            IPlayerFactory playerFactory,
            IPlatformFactory platformFactory,
            IUIFactory uiFactory,
            ISceneLoader sceneLoader,
            ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
            _gameStateMachine = gameStateMachine;
            _playerFactory = playerFactory;
            _platformFactory = platformFactory;
            _uiFactory = uiFactory;
            _sceneLoader = sceneLoader;
        }

        public async void Enter(string payload)
        {
            await _sceneLoader.LoadScene(payload, CreateLevel);
        }

        public void Exit()
        {
        }

        private static void SetCameraTarget(PlayerMove player)
        {
            Camera.main.GetComponentInParent<Follow>().SetTarget(player.gameObject.transform);
        }

        private void CreateLevel()
        {
            PlayerMove player = _playerFactory.Create().GetComponent<PlayerMove>();
            _platformFactory.Create(player);
            _uiFactory.Create(player.GetComponent<PlayerScore>());
            SetCameraTarget(player);
            _saveLoadService.InformLoadDataListeners();
            _gameStateMachine.Enter<GameLoopState>();
        }
    }
}