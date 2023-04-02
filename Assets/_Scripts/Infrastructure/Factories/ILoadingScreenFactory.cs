using Cysharp.Threading.Tasks;
using Infrastructure.UI;
using UnityEngine;

namespace Infrastructure.Factories
{
    public interface ILoadingScreenFactory
    {
        LoadingScreen LoadingScreen { get; }
        public UniTask Create<T>() where T : MonoBehaviour, ILoadingScreen;
    }
}