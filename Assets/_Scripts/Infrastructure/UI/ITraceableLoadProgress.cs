using System;

namespace Infrastructure.UI
{
    public interface ITraceableLoadProgress
    {
        public event Action<float> OnLoaded;
    }
}