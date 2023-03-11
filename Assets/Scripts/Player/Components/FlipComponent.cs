using UnityEngine.InputSystem;
using UnityEngine;
using Zenject;

using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Utils.EventBus;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Player.Components
{
    public sealed class FlipComponent : ComponentBase, ITickable
    {
        public FlipComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
        }

        protected override void StartInternal()
        {
            base.StartInternal();

            _transform = _player.gameObject.transform.IfNullThrowExceptionOrReturn();

            _mainCamera = Camera.main;
            _mouse = Mouse.current;
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnMoveLeftStateEnter, StartToMoveLeft);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightStateEnter, StartToMoveRight);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, StartToMoveLeft);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, StartToMoveRight);
            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, StartToMoveLeft);
            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveRightStateEnter, StartToMoveRight);

            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackStartStateEnter, StartTurnOnMousePosition);
            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackEndStateEnter, EndTurnOnMousePosition);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftStateEnter, StartToMoveLeft);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightStateEnter, StartToMoveRight);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, StartToMoveLeft);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, StartToMoveRight);
            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, StartToMoveLeft);
            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveRightStateEnter, StartToMoveRight);

            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackStartStateEnter, StartTurnOnMousePosition);
            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackEndStateEnter, EndTurnOnMousePosition);
        }

        private void StartToMoveLeft()
        {
            if (!_isLookOnLeft)
            {
                _isLookOnLeft = true;
                _eventBus.RaiseEvent(PlayerEvents.OnFlipPlayerPicture);
            }
        }

        private void StartToMoveRight()
        {
            if (_isLookOnLeft)
            {
                _isLookOnLeft = false;
                _eventBus.RaiseEvent(PlayerEvents.OnFlipPlayerPicture);
            }
        }

        private void StartTurnOnMousePosition()
        {
            _dirty = true;
        }

        private void EndTurnOnMousePosition()
        {
            _dirty = false;
        }

        public void Tick()
        {
            if (_dirty)
            {
                var mousePositionX = _mainCamera.ScreenToWorldPoint(_mouse.position.ReadValue()).x;
                var mousePositionXByPlayerCoordinateSystem = mousePositionX - _transform.position.x;

                if (mousePositionXByPlayerCoordinateSystem < 0 && !_isLookOnLeft)
                {
                    _isLookOnLeft = true;
                    _eventBus.RaiseEvent(PlayerEvents.OnFlipPlayerPicture);
                }
                else if(mousePositionXByPlayerCoordinateSystem > 0 && _isLookOnLeft)
                {
                    _isLookOnLeft = false;
                    _eventBus.RaiseEvent(PlayerEvents.OnFlipPlayerPicture);
                }
            }
        }

        private Transform _transform;

        private bool _isLookOnLeft;
        private bool _dirty;

        private Camera _mainCamera;
        private Mouse _mouse;
    }
}