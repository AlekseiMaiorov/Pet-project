using UnityEngine;

namespace Game.Platform
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private Rigidbody _rigidbody;
        [SerializeField]
        private Collider _scoreTrigger;

        public void ResetTransform()
        {
            _scoreTrigger.gameObject.SetActive(true);
            _rigidbody.gameObject.transform.rotation = Quaternion.identity;
            _rigidbody.velocity = Vector3.zero;
            _rigidbody.angularVelocity = Vector3.zero;
        }
    }
}