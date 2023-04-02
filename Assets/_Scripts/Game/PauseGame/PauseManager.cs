using UnityEngine;

namespace Game.PauseGame
{
    public class PauseManager
    {
        public void Pause()
        {
            Time.timeScale = 0;
        }

        public void Resume()
        {
            Time.timeScale = 1;
        }
    }
}