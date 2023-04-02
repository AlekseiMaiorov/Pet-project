using System;
using Cysharp.Threading.Tasks;

namespace Extension
{
    public static class UniTaskHelper
    {
        public static Action<T> Action<T>(Func<T, UniTaskVoid> asyncAction)
        {
            return t1 => asyncAction(t1).Forget();
        }
    }
}