using System;

namespace Game.Player
{
    public interface IScoreEvents
    {
        public event Action<int> OnScoreChanged;
        public event Action<int> OnMaximumScoreChanged;
    }
}