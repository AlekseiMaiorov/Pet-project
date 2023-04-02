using Game.Common;
using UnityEngine;

namespace Game.Player
{
    public class PlayerScore : MonoBehaviour
    {
        public Score Score => _score;
        private Score _score;
        private TriggerObserver _triggerObserver;

        public void Construct(TriggerObserver triggerObserver)
        {
            _score ??= new Score();

            _triggerObserver = triggerObserver;
            _triggerObserver.TriggerEnter += IncreaseScore;
        }

        private void IncreaseScore(Collider other)
        {
            other.gameObject.SetActive(false);
            _score.Increase();
        }
    }
}