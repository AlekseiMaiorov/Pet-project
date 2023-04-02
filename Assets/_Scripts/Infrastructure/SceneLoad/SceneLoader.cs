using System;
using Cysharp.Threading.Tasks;
using Infrastructure.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Infrastructure.SceneLoad
{
    public class SceneLoader : ISceneLoader, ITraceableLoadProgress
    {
        public event Action<float> OnLoaded;

        public async UniTask LoadScene(string nextScene, Action onLoaded)
        {
            AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(nextScene);

            while (!loadSceneAsync.isDone)
            {
                OnLoaded?.Invoke(loadSceneAsync.progress / 0.9f);
                await UniTask.NextFrame();
            }

            onLoaded?.Invoke();
        }
    }
}