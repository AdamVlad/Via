using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components
{
    public sealed class FlipComponent : ComponentBase
    {
        public FlipComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
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

        private bool _isLookOnLeft;
    }
}