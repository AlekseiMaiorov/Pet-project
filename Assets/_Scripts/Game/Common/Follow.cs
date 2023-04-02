using UnityEngine;

namespace Game.Common
{
    public class Follow : MonoBehaviour
    {
        [SerializeField]
        private float _speed;
        private Transform _target;

        private void LateUpdate()
        {
            if (_target == null)
            {
                return;
            }

            transform.position = Vector3.Lerp(transform.position,
                                              _target.position,
                                              Time.deltaTime * _speed);
        }

        public void SetTarget(Transform target)
        {
            _target = target;
        }
    }
}