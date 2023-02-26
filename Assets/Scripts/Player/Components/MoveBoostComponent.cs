using System;

using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;

namespace Assets.Scripts.Player.Components
{
    public class MoveBoostComponent : ObservableComponentDecorator
    {
        public MoveBoostComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus)
        {
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.MoveLeftBoost, BoostingMove);
            _eventBus.Subscribe(PlayerEvents.MoveRightBoost, BoostingMove);
            _eventBus.Subscribe(PlayerEvents.MoveBoostStopped, StopBoost);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.MoveLeftBoost, BoostingMove);
            _eventBus.Unsubscribe(PlayerEvents.MoveRightBoost, BoostingMove);
            _eventBus.Unsubscribe(PlayerEvents.MoveBoostStopped, StopBoost);
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

        private readonly PlayerSettings _settings;
    }
}