using System.Threading.Tasks;
using Game.StaticData;
using Infrastructure.AssetManagment;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Infrastructure.States
{
    public class LoadAssetsState : IState
    {
        private readonly LevelStaticData _levelStaticData;
        private AddressableProvider _addressableProvider;

        private IGameStateMachine _gameStateMachine;

        public LoadAssetsState(
            IGameStateMachine gameStateMachine,
            AddressableProvider
                addressableProvider,
            LevelStaticData levelStaticData)
        {
            _gameStateMachine = gameStateMachine;
            _addressableProvider = addressableProvider;
            _levelStaticData = levelStaticData;
        }

        public async void Enter()
        {
            await LoadAssets();
            _gameStateMachine.Enter<LoadProgressState>();
        }

        public void Exit()
        {
        }

        private async Task LoadAssets()
        {
            foreach (AssetReference asset in _levelStaticData.Assets)
            {
                await _addressableProvider.Load<Object>(asset);
            }
        }
    }
}