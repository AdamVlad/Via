using System;
using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Settings;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public class PhysicComponent : IPhysicComponent
    {
        public PhysicComponent(
            GameObject player,
            IEventBus<PLayerStates> eventBus,
            PlayerSettings settings,
            PhysicData physicData)
        {
            if (!player.TryGetComponent(out _rigidbody))
            {
                throw new NullReferenceException("PhysicComponent: Rigidbody2D component not set on player");
            }

            _physicData = physicData;
            _eventBus = eventBus;
            _settings = settings;
        }

        public void OnEnable()
        {
            _eventBus.Subscribe(PLayerStates.Idle, Stop);
            _eventBus.Subscribe(PLayerStates.MoveLeft, EnterTheMoveLeftState);
            _eventBus.Subscribe(PLayerStates.MoveRight, EnterTheMoveRightState);
            _eventBus.Subscribe(PLayerStates.JumpStart, Jump);
        }

        public void OnDisable()
        {
            _eventBus.Unsubscribe(PLayerStates.Idle, Stop);
            _eventBus.Unsubscribe(PLayerStates.MoveLeft, EnterTheMoveLeftState);
            _eventBus.Unsubscribe(PLayerStates.MoveRight, EnterTheMoveRightState);
            _eventBus.Unsubscribe(PLayerStates.JumpStart, Jump);
        }

        public void FixedUpdate()
        {
            _physicData.Falling = _rigidbody.velocity.y < 0;

            if (_isMoving)
            {
                Move();
                LimitSpeed();
            }
        }

        private void LimitSpeed()
        {
            if (Mathf.Abs(_rigidbody.velocity.x) > _settings.MaxSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _settings.MaxSpeed;
            }
        }

        private void Stop()
        {
            _isMoving = false;
            _rigidbody.velocity = Vector3.zero;
        }

        private void EnterTheMoveLeftState()
        {
            _directionMultiplier = -1;
            _isMoving = true;
        }

        private void EnterTheMoveRightState()
        {
            _directionMultiplier = 1;
            _isMoving = true;
        }

        private void Move()
        {
            _rigidbody.AddForce(Vector2.right * _directionMultiplier * _rigidbody.mass * _settings.MaxSpeed);
        }

        private void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _rigidbody.mass * _settings.JumpForce);
        }
        
        private readonly Rigidbody2D _rigidbody;
        private readonly IEventBus<PLayerStates> _eventBus;
        private readonly PlayerSettings _settings;

        private readonly PhysicData _physicData;

        private bool _isMoving;
        private int _directionMultiplier;
    }
}

