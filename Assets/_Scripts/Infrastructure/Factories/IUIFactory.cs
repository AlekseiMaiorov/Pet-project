using Game.Player;
using Game.UI;

namespace Infrastructure.Factories
{
    public interface IUIFactory
    {
        HudUI HudUIComponent { get; }
        void Create(PlayerScore playerMove);
    }
}