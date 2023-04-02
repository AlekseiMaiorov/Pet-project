using System;
using Infrastructure.States;
using UnityEngine;

namespace Game.Player
{
    public class PlayerDeath : MonoBehaviour
    {
        public event Action OnDeath;
        private float _deathLine = -6f;
        private IGameStateMachine _gameStateMachine;
        private Rigidbody _rigidbody;
        private Vector3 _spawnPoint;
        [SerializeField]
        private TrailRenderer _trailRenderer;

        public void Construct(
            Vector3 spawnPoint,
            Rigidbody rigidbody,
            IGameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
            _spawnPoint = spawnPoint;
            _rigidbody = rigidbody;
        }

        private void Update()
        {
            if (IsPlayerCrossDeathLine())
            {
                OnDeath?.Invoke();
                _gameStateMachine.Enter<RestartState>();
            }
        }

        public void Restart()
        {
            transform.position = _spawnPoint;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
            _trailRenderer.Clear();
        }

        private bool IsPlayerCrossDeathLine()
        {
            return transform.position.y <= _deathLine;
        }
    }
}