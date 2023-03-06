using UnityEngine;

using Assets.Scripts.Effects.StaffEffects;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Extensions;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components
{
    public class StaffEffectsComponent : ComponentBase
    {
        public StaffEffectsComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
            _orbEffect = Resources.Load<OrbEffect>("PlayerEffects/MagicOrb").IfNullThrowExceptionOrReturn();
            _boostedFireEffect = Resources.Load<BoostedFireEffect>("PlayerEffects/BoostedFire").IfNullThrowExceptionOrReturn();     // сделай скрипт, который находит путь к этому файлу автоматически
            _powerUpFireBulletEffect = Resources.Load<PowerUpFireBulletEffect>("PlayerEffects/PowerupFireBullet").IfNullThrowExceptionOrReturn();
        }

        public void Start(Transform effectsPointPosition)
        {
            _orbEffect = Object.Instantiate(_orbEffect, effectsPointPosition);
            _boostedFireEffect = Object.Instantiate(_boostedFireEffect, effectsPointPosition);
            _powerUpFireBulletEffect = Object.Instantiate(_powerUpFireBulletEffect, effectsPointPosition);

            _boostedFireEffect.Deactivate();
            _powerUpFireBulletEffect.Deactivate();
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
            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackEndStateEnter, ActivateOrbEffect);
            _eventBus.Subscribe(PlayerEvents.OnFallStateEnter, ActivateOrbEffect);

            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackStartStateEnter, ActivatePowerUpFireBulletEffect);
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
            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackEndStateEnter, ActivateOrbEffect);
            _eventBus.Unsubscribe(PlayerEvents.OnFallStateEnter, ActivateOrbEffect);

            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackStartStateEnter, ActivatePowerUpFireBulletEffect);
        }

        private void ActivateBoostedFireEffect()
        {
            _orbEffect.Deactivate();
            _powerUpFireBulletEffect.Deactivate();

            _boostedFireEffect.Activate();
        }

        private void ActivateOrbEffect()
        {
            _boostedFireEffect.Deactivate();
            _powerUpFireBulletEffect.Deactivate();

            _orbEffect.Activate();
        }

        private void ActivatePowerUpFireBulletEffect()
        {
            _boostedFireEffect.Deactivate();
            _orbEffect.Deactivate();

            _powerUpFireBulletEffect.Activate();
        }

        private BoostedFireEffect _boostedFireEffect;
        private OrbEffect _orbEffect;
        private PowerUpFireBulletEffect _powerUpFireBulletEffect;
    }
}