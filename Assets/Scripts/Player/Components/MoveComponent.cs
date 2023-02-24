using System;
using UnityEngine;
using Zenject;

using Assets.Scripts.Extensions;
using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Patterns.Observer;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Player.ComponentsData.Interfaces;

namespace Assets.Scripts.Player.Components
{
    public sealed class MoveComponent : ComponentBase, IObserver, IFixedTickable
    {
        public MoveComponent(
            IEventBus<PlayerStates> eventBus,
            MoveBoostComponent moveBoostComponent,
            GameObject player,
            PlayerSettings settings) : base(eventBus)
        {
            _boostingObservable = moveBoostComponent.IfNullThrowExceptionOrReturn();
            _transform = player.IfNullThrowExceptionOrReturn().transform;
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _boostingObservable.AddObserver(this);

            _eventBus.Subscribe(PlayerStates.MoveLeft, PrepareToMoveLeft);
            _eventBus.Subscribe(PlayerStates.MoveRight, PrepareToMoveRight);
            _eventBus.Subscribe(PlayerStates.MoveLeftWhenFlying, PrepareToMoveLeft);
            _eventBus.Subscribe(PlayerStates.MoveRightWhenFlying, PrepareToMoveRight);
            _eventBus.Subscribe(PlayerStates.MoveStopped, StopMove);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _boostingObservable.RemoveObserver(this);

            _eventBus.Unsubscribe(PlayerStates.MoveLeft, PrepareToMoveLeft);
            _eventBus.Unsubscribe(PlayerStates.MoveRight, PrepareToMoveRight);
            _eventBus.Unsubscribe(PlayerStates.MoveLeftWhenFlying, PrepareToMoveLeft);
            _eventBus.Unsubscribe(PlayerStates.MoveRightWhenFlying, PrepareToMoveRight);
            _eventBus.Unsubscribe(PlayerStates.MoveStopped, StopMove);
        }

        public void FixedTick()
        {
            if (_dirty)
            {
                _transform.Translate(
                    Vector3.right * 
                    _direction * 
                    _settings.NormalSpeed *
                    _moveBoostDataHashed.BoostMultiplier *
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

        private readonly Transform _transform;
        private readonly PlayerSettings _settings;

        private MoveBoostData _moveBoostDataHashed;
        private bool _dirty;
        private int _direction;
    }
}