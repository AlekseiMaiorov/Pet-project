using Infrastructure.States;
using Zenject;

namespace Infrastructure.Factories
{
    internal class StatesFactory : IStatesFactory
    {
        private readonly IInstantiator _instantiator;

        public StatesFactory(IInstantiator instantiator)
        {
            _instantiator = instantiator;
        }

        public T Create<T>() where T : IExitableState
        {
            return _instantiator.Instantiate<T>();
        }
    }
}