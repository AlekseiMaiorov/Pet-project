using Infrastructure.States;
using Zenject;

namespace Infrastructure.Starting
{
    public class ProjectStarter : IInitializable
    {
        private IGameStateMachine _gameStateMachine;

        public ProjectStarter(IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Initialize()
        {
            _gameStateMachine.Enter<BootstrapState>();
        }
    }
}