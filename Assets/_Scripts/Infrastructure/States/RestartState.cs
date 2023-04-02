using Game.PauseGame;
using Game.Player;
using Infrastructure.Factories;

namespace Infrastructure.States
{
    public class RestartState : IState
    {
        private readonly IGameStateMachine _gameStateMachine;
        private readonly IPlatformFactory _platformFactory;
        private PauseManager _pauseManager;
        private IPlayerFactory _playerFactory;
        private IUIFactory _uiFactory;

        public RestartState(
            IGameStateMachine gameStateMachine,
            IPlatformFactory platformFactory,
            IPlayerFactory
                playerFactory,
            IUIFactory uiFactory,
            PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
            _uiFactory = uiFactory;
            _playerFactory = playerFactory;
            _gameStateMachine = gameStateMachine;
            _platformFactory = platformFactory;
        }

        public void Enter()
        {
            _gameStateMachine.Enter<GameLoopState>();
        }

        public void Exit()
        {
            RestartAll();
        }

        private void RestartAll()
        {
            _platformFactory.PlatformsMover.Restart();
            _uiFactory.HudUIComponent.Reset();
            _pauseManager.Resume();
            _playerFactory.Player.GetComponent<PlayerDeath>().Restart();
            _playerFactory.Player.GetComponent<PlayerScore>().Score.ResetScore();
        }
    }
}