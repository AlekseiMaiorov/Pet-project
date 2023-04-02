using Game.PauseGame;
using Infrastructure.States;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class HudUI : MonoBehaviour
    {
        [SerializeField]
        private Button _restart;
        
        [Space(1f)]
        [Header("Game Group")]
        [SerializeField]
        private GameObject _gameGroup;
        [SerializeField]
        private Button _pause;
        
        [Space(1f)]
        [Header("Pause Group")]
        [SerializeField]
        private GameObject _pauseGroup;
        [SerializeField]
        private Button _resume;
        [SerializeField]
        private Button _exit;
        
        private IGameStateMachine _gameStateMachine;
        private PauseManager _pauseManager;

        [Inject]
        public void Construct(
            IGameStateMachine gameStateMachine,
            PauseManager pauseManager)
        {
            _pauseManager = pauseManager;
            _gameStateMachine = gameStateMachine;

            _resume.onClick.AddListener(ResumeGame);
            _pause.onClick.AddListener(PauseGame);
            _exit.onClick.AddListener(ExitGame);
            _restart.onClick.AddListener(RestartGame);
        }
        

        public void Reset()
        {
            SetActivePauseGroup(false);
            SetActiveGameGroup(true);
        }

        private void ExitGame()
        {
            _gameStateMachine.Enter<ExitGameState>();
        }

        private void PauseGame()
        {
            SetActivePauseGroup(true);
            SetActiveGameGroup(false);
            _pauseManager.Pause();
        }

        private void RestartGame()
        {
            _gameStateMachine.Enter<RestartState>();
        }

        private void ResumeGame()
        {
            SetActivePauseGroup(false);
            SetActiveGameGroup(true);
            _pauseManager.Resume();
        }

        private void SetActiveGameGroup(bool b)
        {
            _gameGroup.SetActive(b);
        }

        private void SetActivePauseGroup(bool b)
        {
            _pauseGroup.SetActive(b);
        }
    }
}