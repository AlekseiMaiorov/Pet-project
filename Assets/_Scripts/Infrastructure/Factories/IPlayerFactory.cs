using UnityEngine;

namespace Infrastructure.Factories
{
    public interface IPlayerFactory
    {
        GameObject Player { get; }
        GameObject Create();
    }
}