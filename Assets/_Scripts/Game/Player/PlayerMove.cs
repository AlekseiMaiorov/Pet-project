using System;
using Game.Common;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Game.Player
{
    [SelectionBase]
    public class PlayerMove : MonoBehaviour
    {
        public event Action OnJumped;
        private CollisionObserver _collisionObserver;
        private float _direction;
        private PlayerInput _input;
        private bool _isAllowJump;
        private bool _isGrounded;
        private bool _isMoving;
        private float _maximumGripAngle = 60f;
        private Vector3 _playerSpawnPoint;
        private Rigidbody _rigidbody;
        private Settings _settings;

        public void Construct(
            CollisionObserver collisionObserver,
            Rigidbody rigidbody,
            Settings settings,
            PlayerInput playerInput)
        {
            _collisionObserver = collisionObserver;
            _rigidbody = rigidbody;
            _settings = settings;
            _input = playerInput;

            BindInputActions();
            BindCollisionObserver();
        }

        private void FixedUpdate()
        {
            if (_isMoving)
            {
                TorqueSphere();
            }
            
            LimitTopSpeed();
        }

        private void CollisionExit(Collision collision)
        {
            _isGrounded = false;
        }

        private void CollisionStay(Collision collision)
        {
            _isGrounded = true;
            _isAllowJump = CheckJumpPossibility(collision);
        }

        private void Jump(InputAction.CallbackContext callbackContext)
        {
            if (_isGrounded && _isAllowJump)
            {
                OnJumped?.Invoke();
                _rigidbody.AddForce(0f, _settings.JumpSpeed, 0f, ForceMode.VelocityChange);
            }
        }

        private void ResetDirection(InputAction.CallbackContext callbackContext)
        {
            _direction = 0f;
            _isMoving = false;
        }

        private void SetDirection(InputAction.CallbackContext callbackContext)
        {
            _direction = callbackContext.ReadValue<float>();
            _isMoving = true;
        }

        private void BindCollisionObserver()
        {
            _collisionObserver.CollisionStay += CollisionStay;
            _collisionObserver.CollisionExit += CollisionExit;
        }

        private void BindInputActions()
        {
            _input.actions["Move"].performed += SetDirection;
            _input.actions["Move"].canceled += ResetDirection;
            _input.actions["Jump"].started += Jump;
        }

        private bool CheckJumpPossibility(Collision collision)
        {
            float collisionAngle = Vector3.Angle(collision.contacts[0].normal, Vector3.up);
            return collisionAngle < _maximumGripAngle;
        }

        private void LimitTopSpeed()
        {
            if (_rigidbody.velocity.magnitude > _settings.MaxSpeed)
            {
                _rigidbody.velocity = Vector3.ClampMagnitude(_rigidbody.velocity, _settings.MaxSpeed);
            }
        }

        private void TorqueSphere()
        {
            _rigidbody.maxAngularVelocity = _settings.MaxAngularVelocity;
            float speedZ = _direction * _settings.TorqueSpeed;
            _rigidbody.AddTorque(0, 0, speedZ, ForceMode.VelocityChange);
        }

        [Serializable]
        public class Settings
        {
            public float JumpSpeed;
            public float MaxAngularVelocity;
            public float MaxSpeed;
            public float TorqueSpeed;
        }
    }
}