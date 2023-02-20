using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;

namespace Assets.Scripts.Player.Components
{
    public sealed class FlipComponent : ComponentBase
    {
        // Потом в файлах сохранения персонажа запоминать его поворот и передавать в конструкторе. Не забудь!
        public FlipComponent(IEventBus<PlayerStates> eventBus) : base(eventBus)
        {
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerStates.MoveLeft, StartToMoveLeft);
            _eventBus.Subscribe(PlayerStates.MoveRight, StartToMoveRight);
            _eventBus.Subscribe(PlayerStates.MoveLeftWhenFlying, StartToMoveLeft);
            _eventBus.Subscribe(PlayerStates.MoveRightWhenFlying, StartToMoveRight);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerStates.MoveLeft, StartToMoveLeft);
            _eventBus.Unsubscribe(PlayerStates.MoveRight, StartToMoveRight);
            _eventBus.Unsubscribe(PlayerStates.MoveLeftWhenFlying, StartToMoveLeft);
            _eventBus.Unsubscribe(PlayerStates.MoveRightWhenFlying, StartToMoveRight);
        }

        private void StartToMoveLeft()
        {
            if (!_isLookOnLeft)
            {
                _isLookOnLeft = true;
                _eventBus.RaiseEvent(PlayerStates.Flip);
            }
        }

        private void StartToMoveRight()
        {
            if (_isLookOnLeft)
            {
                _isLookOnLeft = false;
                _eventBus.RaiseEvent(PlayerStates.Flip);
            }
        }

        private bool _isLookOnLeft;
    }
}