using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;

namespace Assets.Scripts.Player.Components
{
    public sealed class FlipComponent : ComponentBase
    {
        // Потом в файлах сохранения персонажа запоминать его поворот и передавать в конструкторе. Не забудь!
        public FlipComponent(IEventBus<PlayerEvents> eventBus) : base(eventBus)
        {
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnMoveLeftStateEnter, StartToMoveLeft);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightStateEnter, StartToMoveRight);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, StartToMoveLeft);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, StartToMoveRight);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftStateEnter, StartToMoveLeft);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightStateEnter, StartToMoveRight);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, StartToMoveLeft);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, StartToMoveRight);
        }

        private void StartToMoveLeft()
        {
            if (!_isLookOnLeft)
            {
                _isLookOnLeft = true;
                _eventBus.RaiseEvent(PlayerEvents.Flip);
            }
        }

        private void StartToMoveRight()
        {
            if (_isLookOnLeft)
            {
                _isLookOnLeft = false;
                _eventBus.RaiseEvent(PlayerEvents.Flip);
            }
        }

        private bool _isLookOnLeft;
    }
}