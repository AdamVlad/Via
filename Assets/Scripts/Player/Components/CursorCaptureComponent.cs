using UnityEngine;
using UnityEngine.InputSystem;

using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components
{
    public sealed class CursorCaptureComponent : ObservableComponentDecorator
    {
        public CursorCaptureComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
        }

        protected override void StartInternal()
        {
            base.StartInternal();

            _mainCamera = Camera.main;
            _mouse = Mouse.current;
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackStartStateEnter, NotifyCursorCoordinates);
            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackEndStateEnter, NotifyCursorCoordinates);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackStartStateEnter, NotifyCursorCoordinates);
            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackEndStateEnter, NotifyCursorCoordinates);
        }

        private void NotifyCursorCoordinates()
        {
            Notify(new CursorData
            {
                Coordinates = _mainCamera.ScreenToWorldPoint(_mouse.position.ReadValue())
            });
        }

        private Camera _mainCamera;
        private Mouse _mouse;
    }
}