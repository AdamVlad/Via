using System;

using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Patterns.Observer;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;

namespace Assets.Scripts.Player.Components
{
    public class MoveBoostComponent : ObservableComponentDecorator
    {
        public MoveBoostComponent(
            IEventBus<PlayerStates> eventBus,
            PlayerSettings settings) : base(eventBus)
        {
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerStates.MoveBoost, BoostingMove);
            _eventBus.Subscribe(PlayerStates.MoveBoostStopped, StopBoost);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerStates.MoveBoost, BoostingMove);
            _eventBus.Unsubscribe(PlayerStates.MoveBoostStopped, StopBoost);
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