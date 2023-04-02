using Infrastructure.SaveLoad;
using UnityEditor;
using UnityEngine;

namespace Infrastructure.States
{
    public class ExitGameState : IState
    {
        private ISaveLoadService _saveLoadService;

        public ExitGameState(ISaveLoadService saveLoadService)
        {
            _saveLoadService = saveLoadService;
        }

        public void Enter()
        {
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
            _saveLoadService.Save();
            Application.Quit();
        }

        public void Exit()
        {
        }
    }
}