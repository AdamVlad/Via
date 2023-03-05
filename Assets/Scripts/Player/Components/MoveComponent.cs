using UnityEngine;
using Zenject;

using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Player.ComponentsData.Interfaces;
using Assets.Scripts.Utils.EventBus;
using Assets.Scripts.Utils.Observer;

namespace Assets.Scripts.Player.Components
{
    public sealed class MoveComponent : ComponentBase, IObserver, IFixedTickable
    {
        public MoveComponent(
            IEventBus<PlayerEvents> eventBus,
            MoveBoostComponent moveBoostComponent,
            PlayerSettings settings) : base(eventBus, settings)
        {
            _boostingObservable = moveBoostComponent.IfNullThrowExceptionOrReturn();
        }

        protected override void StartInternal()
        {
            base.StartInternal();

            _transform = _player.gameObject.transform;
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _boostingObservable.AddObserver(this);

            _eventBus.Subscribe(PlayerEvents.OnMoveLeftStateEnter, PrepareToMoveLeft);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightStateEnter, PrepareToMoveRight);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, PrepareToMoveLeft);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, PrepareToMoveRight);
            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, PrepareToMoveLeft);
            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveRightStateEnter, PrepareToMoveRight);
            _eventBus.Subscribe(PlayerEvents.OnStoppingMove, StopMove);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _boostingObservable.RemoveObserver(this);

            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftStateEnter, PrepareToMoveLeft);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightStateEnter, PrepareToMoveRight);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftWhenFallingStateEnter, PrepareToMoveLeft);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightWhenFallingStateEnter, PrepareToMoveRight);
            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, PrepareToMoveLeft);
            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveRightStateEnter, PrepareToMoveRight);
            _eventBus.Unsubscribe(PlayerEvents.OnStoppingMove, StopMove);
        }

        public void FixedTick()
        {
            if (_dirty)
            {
                _transform.Translate(
                    Vector3.right * 
                    _direction *
                    _moveBoostDataHashed.BoostMultiplier *
                    _settings.NormalSpeed *
                    Time.fixedDeltaTime);
            }
        }

        public void Update(ref IData data)
        {
            if (data is MoveBoostData boostData)
            {
                _moveBoostDataHashed = boostData;
            }
        }

        private void PrepareToMoveRight()
        {
            _direction = 1;
            _dirty = true;
        }

        private void PrepareToMoveLeft()
        {
            _direction = -1;
            _dirty = true;
        }

        private void StopMove()
        {
            _dirty = false;
        }

        private readonly ObservableComponentDecorator _boostingObservable;

        private Transform _transform;

        private MoveBoostData _moveBoostDataHashed;
        private bool _dirty;
        private int _direction;
    }
}