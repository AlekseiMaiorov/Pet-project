using System;
using Game.Common;
using UnityEngine;

namespace Game.Player
{
    public class PlayerSound : MonoBehaviour
    {
        private SoundClips _clips;
        private CollisionObserver _collisionObserver;
        private PlayerDeath _playerDeath;
        private PlayerMove _playerMove;
        [SerializeField]
        private AudioSource _collision;
        [SerializeField]
        private AudioSource _death;
        [SerializeField]
        private AudioSource _jump;

        public void Construct(
            SoundClips clips,
            PlayerMove movement,
            CollisionObserver collisionObserver,
            PlayerDeath playerDeath)
        {
            _collisionObserver = collisionObserver;
            _clips = clips;
            _playerDeath = playerDeath;
            _playerMove = movement;

            _jump.clip = _clips.Jump;
            _collision.clip = _clips.Collision;
            _death.clip = _clips.Death;

            _playerDeath.OnDeath += DeathsoundPlay;
            _playerMove.OnJumped += JumpSoundPlay;
            _collisionObserver.CollisionEnter += CollisionSoundPlay;
        }

        private void CollisionSoundPlay(Collision collision)
        {
            _collision.Play();
        }

        private void DeathsoundPlay()
        {
            _death.Play();
        }

        private void JumpSoundPlay()
        {
            _jump.Play();
        }

        [Serializable]
        public class SoundClips
        {
            public AudioClip Jump;
            public AudioClip Collision;
            public AudioClip Death;
        }
    }
}