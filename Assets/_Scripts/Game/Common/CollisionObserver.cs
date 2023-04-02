using System;
using UnityEngine;

namespace Game.Common
{
    public class CollisionObserver : MonoBehaviour
    {
        public event Action<Collision> CollisionEnter;
        public event Action<Collision> CollisionExit;
        public event Action<Collision> CollisionStay;

        private void OnCollisionEnter(Collision collision)
        {
            CollisionEnter?.Invoke(collision);
        }

        private void OnCollisionExit(Collision collision)
        {
            CollisionExit?.Invoke(collision);
        }

        private void OnCollisionStay(Collision collision)
        {
            CollisionStay?.Invoke(collision);
        }
    }
}