using UnityEngine;

using Assets.Scripts.Effects.StaffEffects;
using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Player.Components
{
    public class StaffEffectsComponent : ComponentBase
    {
        public StaffEffectsComponent(IEventBus<PlayerEvents> eventBus) : base(eventBus)
        {
            _orbEffect = Resources.Load<OrbEffect>("PlayerEffects/MagicOrb").IfNullThrowExceptionOrReturn();
            _boostedFireEffect = Resources.Load<BoostedFireEffect>("PlayerEffects/BoostedFire").IfNullThrowExceptionOrReturn();     // сделай скрипт, который находит путь к этому файлу автоматически
        }

        public void Start(Transform effectsPointPosition)
        {
            _orbEffect = Object.Instantiate(_orbEffect, effectsPointPosition);
            _boostedFireEffect = Object.Instantiate(_boostedFireEffect, effectsPointPosition);

            _boostedFireEffect.Deactivate();
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, ActivateBoostedFireEffect);
            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveRightStateEnter, ActivateBoostedFireEffect);

            _eventBus.Subscribe(PlayerEvents.OnIdleStateEnter, ActivateOrbEffect);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftStateEnter, ActivateOrbEffect);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightStateEnter, ActivateOrbEffect);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, ActivateOrbEffect);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, ActivateOrbEffect);
            _eventBus.Subscribe(PlayerEvents.OnJumpStartStateEnter, ActivateOrbEffect);
            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackStartStateEnter, ActivateOrbEffect);
            _eventBus.Subscribe(PlayerEvents.OnFallStateEnter, ActivateOrbEffect);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, ActivateBoostedFireEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveRightStateEnter, ActivateBoostedFireEffect);

            _eventBus.Unsubscribe(PlayerEvents.OnIdleStateEnter, ActivateOrbEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftStateEnter, ActivateOrbEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightStateEnter, ActivateOrbEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, ActivateOrbEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, ActivateOrbEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnJumpStartStateEnter, ActivateOrbEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackStartStateEnter, ActivateOrbEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnFallStateEnter, ActivateOrbEffect);
        }

        private void ActivateBoostedFireEffect()
        {
            _orbEffect.Deactivate();
            _boostedFireEffect.Activate();
        }

        private void ActivateOrbEffect()
        {
            _boostedFireEffect.Deactivate();
            _orbEffect.Activate();
        }

        private BoostedFireEffect _boostedFireEffect;
        private OrbEffect _orbEffect;
    }
}
