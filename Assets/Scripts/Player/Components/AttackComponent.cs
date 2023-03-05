using UnityEngine;
using UnityEngine.Pool;

using Assets.Scripts.Entities.Bullets;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Utils.EventBus;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Player.Components
{
    public class AttackComponent : ComponentBase
    {
        public AttackComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings,
            IObjectPool<FireBullet> fireBulletsPool) : base(eventBus, settings)
        {
            _fireBulletsPool = fireBulletsPool; // проверить на нул
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackEndStateEnter, ShootFireBullet);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackEndStateEnter, ShootFireBullet);
        }

        public void Start(Transform shootStartPoint)
        {
            _shootStartPoint = shootStartPoint.IfNullThrowExceptionOrReturn();
        }

        private void ShootFireBullet()
        {
            var fireBullet = _fireBulletsPool.Get();
            fireBullet.transform.position = _shootStartPoint.position;
        }

        private readonly IObjectPool<FireBullet> _fireBulletsPool;

        private Transform _shootStartPoint;
    }
}