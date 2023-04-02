using Game.Player;
using Game.UI;
using Infrastructure.AssetManagment;
using UnityEngine;
using Zenject;

namespace Infrastructure.Factories
{
    public class UIFactory : IUIFactory
    {
        public HudUI HudUIComponent => _hudUIComponent;
        private readonly AddressableProvider _addressableProvider;
        private DiContainer _container;
        private HudUI _hudUIComponent;
        private GameObject _ui;

        public UIFactory(AddressableProvider addressableProvider, DiContainer container)
        {
            _container = container;
            _addressableProvider = addressableProvider;
        }

        public void Create(PlayerScore playerScore)
        {
            var uiPrefab = _addressableProvider.Get<GameObject>(AssetNames.UI_PREFAB);
            _ui = Object.Instantiate(uiPrefab);
            _container.InjectGameObject(_ui);

            var uiScorePresenter = _ui.GetComponentInChildren<ScorePresenter>();
            uiScorePresenter.Construct(playerScore.Score);

            _hudUIComponent = _ui.GetComponentInChildren<HudUI>();
        }
    }
}