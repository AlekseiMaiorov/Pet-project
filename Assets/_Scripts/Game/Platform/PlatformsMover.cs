using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Platform
{
    public class PlatformsMover : MonoBehaviour
    {
        private bool _isLaunchPlatformSetActive;
        private GameObject _launchPlatform;
        private Vector3 _platformNextPosition;
        private Queue<Platform> _platforms;
        private Transform _playerPosition;
        private Settings _settings;
        private Vector3 _triggerNextPosition;

        public void Construct(
            List<Platform> platforms,
            GameObject firstPlatform,
            Settings settings,
            Transform playerPosition)
        {
            _playerPosition = playerPosition;
            _launchPlatform = firstPlatform;
            _settings = settings;

            _platforms = new Queue<Platform>(platforms);

            RestartPlatformsToStart();
        }

        private void Update()
        {
            if (IsPlayerCrossedTrigger())
            {
                if (_isLaunchPlatformSetActive)
                {
                    SetActiveLaunchPlatform(false);
                }

                MovePlatformNext();
                MoveTriggerNext();
            }
        }

        public void Restart()
        {
            RestartPlatformsToStart();
            RestartTriggerPosition();
            SetActiveLaunchPlatform(true);
        }

        private bool IsPlayerCrossedTrigger()
        {
            return _playerPosition.position.x < _triggerNextPosition.x;
        }

        private void MovePlatformNext()
        {
            Platform platform = _platforms.Dequeue();
            platform.ResetTransform();
            platform.transform.position = _platformNextPosition;
            _platformNextPosition += _settings.DistanceBetweenPlatforms;
            _platforms.Enqueue(platform);
        }

        private void MoveTriggerNext()
        {
            _triggerNextPosition += _settings.TriggerOffset;
        }

        private void RestartPlatformsToStart()
        {
            _platformNextPosition = _settings.PlatformStartPosition;
            for (int i = 0; i < _platforms.Count; i++)
            {
                MovePlatformNext();
            }

            RestartTriggerPosition();
        }

        private void RestartTriggerPosition()
        {
            _triggerNextPosition = _settings.TriggerStartPosition;
        }

        private void SetActiveLaunchPlatform(bool b)
        {
            _isLaunchPlatformSetActive = b;
            _launchPlatform.gameObject.SetActive(b);
        }

        [Serializable]
        public class Settings
        {
            public Vector3 DistanceBetweenPlatforms;
            public Vector3 PlatformStartPosition;
            public Vector3 TriggerOffset;
            public Vector3 TriggerStartPosition;
        }
    }
}