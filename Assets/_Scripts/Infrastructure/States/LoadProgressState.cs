using Infrastructure.PersistentProgress;
using Infrastructure.SaveLoad;

namespace Infrastructure.States
{
    public class LoadProgressState : IState
    {
        private IGameStateMachine _gameStateMachine;
        private IPersistantProgress _persistantProgress;
        private ISaveLoadService _saveLoadProgress;

        public LoadProgressState(
            IGameStateMachine gameStateMachine,
            IPersistantProgress persistantProgress,
            ISaveLoadService saveLoadProgress)
        {
            _gameStateMachine = gameStateMachine;
            _persistantProgress = persistantProgress;
            _saveLoadProgress = saveLoadProgress;
        }

        public void Enter()
        {
            _persistantProgress.Progress = _saveLoadProgress.LoadSavedData() ?? new PlayerProgress();
            _gameStateMachine.Enter<LoadLevelState, string>(_persistantProgress.Progress.Level);
        }

        public void Exit()
        {
        }
    }
}