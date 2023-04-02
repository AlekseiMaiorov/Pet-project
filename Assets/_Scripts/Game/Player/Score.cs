using System;
using Infrastructure.PersistentProgress;
using Infrastructure.States;

namespace Game.Player
{
    public class Score : ISaveLoadData, IScoreEvents
    {
        public event Action<int> OnMaximumScoreChanged;
        public event Action<int> OnScoreChanged;
        private int _maximumScore;
        private int _score;

        public void Increase(int points = 1)
        {
            _score += points;
            UpdateMaximumScore();
            OnScoreChanged?.Invoke(_score);
        }

        public void LoadData(PlayerProgress progress)
        {
            _maximumScore = progress.MaximumScore;
            OnMaximumScoreChanged?.Invoke(_maximumScore);
        }

        public void ResetScore()
        {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
        }

        public void SaveData(PlayerProgress progress)
        {
            progress.MaximumScore = _maximumScore;
        }

        private void UpdateMaximumScore()
        {
            if (_score > _maximumScore)
            {
                _maximumScore = _score;
                OnMaximumScoreChanged?.Invoke(_maximumScore);
            }
        }
    }
}