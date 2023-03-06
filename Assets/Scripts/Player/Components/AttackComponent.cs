using System;
using UnityEngine;
using UnityEngine.Pool;

using Assets.Scripts.Entities.Bullets;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Utils.EventBus;
using Assets.Scripts.Extensions;
using Assets.Scripts.Player.ComponentsData.Interfaces;
using Assets.Scripts.Utils.Observer;
using Assets.Scripts.Player.ComponentsData;

namespace Assets.Scripts.Player.Components
{
    public class AttackComponent : ComponentBase,  IObserver
    {
        public AttackComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings,
            IObjectPool<FireBullet> fireBulletsPool,
            CursorCaptureComponent cursorCaptureComponent) : base(eventBus, settings)
        {
            _fireBulletsPool = fireBulletsPool ?? throw new NullReferenceException("AttackComponent: IObjectPool<FireBullet> not set");
            _cursorCaptureComponent = cursorCaptureComponent.IfNullThrowExceptionOrReturn();

            _cursorData = new CursorData();
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _cursorCaptureComponent.AddObserver(this);

            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackEndStateEnter, ShootFireBullet);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _cursorCaptureComponent.RemoveObserver(this);

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
            fireBullet.GiveMovementTo(_cursorData.Coordinates);
        }

        public void Update(ref IData data)
        {
            if (data is CursorData cursorData)
            {
                _cursorData = cursorData;
            }
        }

        private readonly IObjectPool<FireBullet> _fireBulletsPool;
        private readonly CursorCaptureComponent _cursorCaptureComponent;

        private Transform _shootStartPoint;
        private CursorData _cursorData;
    }
}