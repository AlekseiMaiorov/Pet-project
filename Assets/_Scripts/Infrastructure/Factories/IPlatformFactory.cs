using Game.Platform;
using Game.Player;

namespace Infrastructure.Factories
{
    public interface IPlatformFactory
    {
        PlatformsMover PlatformsMover { get; }
        void Create(PlayerMove playerMove);
    }
}