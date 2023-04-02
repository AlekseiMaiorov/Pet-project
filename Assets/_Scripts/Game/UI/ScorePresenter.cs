using Game.Player;
using TMPro;
using UnityEngine;

namespace Game.UI
{
    public class ScorePresenter : MonoBehaviour
    {
        public TextMeshProUGUI MaximumScoreText;
        public TextMeshProUGUI ScoreText;

        private IScoreEvents _scoreEvents;

        public void Construct(IScoreEvents scoreEvents)
        {
            _scoreEvents = scoreEvents;
            _scoreEvents.OnMaximumScoreChanged += MaximumScoreChanged;
            _scoreEvents.OnScoreChanged += ScoreChanged;
        }

        private void MaximumScoreChanged(int maximumScore)
        {
            MaximumScoreText.text = maximumScore.ToString();
        }

        private void ScoreChanged(int score)
        {
            ScoreText.text = score.ToString();
        }
    }
}