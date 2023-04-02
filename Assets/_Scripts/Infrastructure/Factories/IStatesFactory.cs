using Infrastructure.States;

namespace Infrastructure.Factories
{
    public interface IStatesFactory
    {
        public T Create<T>() where T : IExitableState;
    }
}