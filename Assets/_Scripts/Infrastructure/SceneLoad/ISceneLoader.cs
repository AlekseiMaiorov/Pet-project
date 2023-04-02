using System;
using Cysharp.Threading.Tasks;

namespace Infrastructure.SceneLoad
{
    public interface ISceneLoader
    {
        UniTask LoadScene(string nextScene, Action onLoaded = null);
    }
}