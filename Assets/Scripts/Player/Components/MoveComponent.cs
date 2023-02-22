using System;
using UnityEngine;
using Zenject;

using Assets.Scripts.Extensions;
using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;

namespace Assets.Scripts.Player.Components
{
    public sealed class MoveComponent : ComponentBase, IFixedTickable
    {
        public MoveComponent(
            IEventBus<PlayerStates> eventBus,
            GameObject player,
            PlayerSettings settings) : base(eventBus)
        {
            _transform = player.IfNullThrowExceptionOrReturn().transform;
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerStates.MoveLeft, PrepareToMoveLeft);
            _eventBus.Subscribe(PlayerStates.MoveRight, PrepareToMoveRight);
            _eventBus.Subscribe(PlayerStates.MoveLeftWhenFlying, PrepareToMoveLeft);
            _eventBus.Subscribe(PlayerStates.MoveRightWhenFlying, PrepareToMoveRight);
            _eventBus.Subscribe(PlayerStates.Stopped, StopMove);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerStates.MoveLeft, PrepareToMoveLeft);
            _eventBus.Unsubscribe(PlayerStates.MoveRight, PrepareToMoveRight);
            _eventBus.Unsubscribe(PlayerStates.MoveLeftWhenFlying, PrepareToMoveLeft);
            _eventBus.Unsubscribe(PlayerStates.MoveRightWhenFlying, PrepareToMoveRight);
            _eventBus.Unsubscribe(PlayerStates.Stopped, StopMove);
        }

        public void FixedTick()
        {
            if (_dirty)
            {
                _transform.Translate(Vector3.right * _direction * _settings.MaxSpeed * Time.fixedDeltaTime);
            }
        }

        private void PrepareToMoveRight()
        {
            _direction = 1;
            _dirty = true;
        }

        private void PrepareToMoveLeft()
        {
            _direction = -1;
            _dirty = true;
        }

        private void StopMove()
        {
            _dirty = false;
        }

        private readonly Transform _transform;
        private readonly PlayerSettings _settings;

        private bool _dirty;
        private int _direction;
    }
}