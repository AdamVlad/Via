using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components
{
    public class MoveBoostComponent : ObservableComponentDecorator
    {
        public MoveBoostComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, BoostingMove);
            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveRightStateEnter, BoostingMove);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftStateEnter, StopBoost);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightStateEnter, StopBoost);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, StopBoost);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, StopBoost);
            _eventBus.Subscribe(PlayerEvents.OnFallStateEnter, StopBoost);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, BoostingMove);
            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveRightStateEnter, BoostingMove);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftStateEnter, StopBoost);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightStateEnter, StopBoost);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, StopBoost);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, StopBoost);
            _eventBus.Unsubscribe(PlayerEvents.OnFallStateEnter, StopBoost);
        }

        private void BoostingMove()
        {
            Notify(new MoveBoostData
            {
                BoostMultiplier = _settings.BoostSpeedMultiplier
            });
        }

        private void StopBoost()
        {
            Notify(new MoveBoostData
            {
                BoostMultiplier = 1
            });
        }
    }
}