using System;
using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.States;
using Zenject;

namespace Assets.Scripts.Player.Components
{
    public class StateComponent : IStateComponent
    {
        public StateComponent(
            StateMachine stateMachine,
            [Inject(Id = "PlayerIdleState")] StateBase idleState,
            [Inject(Id = "PlayerWalkState")] StateBase walkState,
            [Inject(Id = "PlayerJumpStartState")] StateBase jumpStartState,
            [Inject(Id = "PlayerFlyingState")] StateBase flyingState,
            PlayerInputData inputData,
            PlayerCollisionData collisionData,
            PlayerPhysicData physicData)
        {
            _stateMachine = stateMachine;

            _idleState = idleState;
            _walkState = walkState;
            _jumpStartState = jumpStartState;
            _flyingState = flyingState;

            _inputData = inputData;
            _collisionData = collisionData;
            _physicData = physicData;
        }

        public void Start()
        {
            if (_stateMachine == null ||
                _idleState == null ||
                _walkState == null ||
                _jumpStartState == null ||
                _flyingState == null)
            {
                throw new NullReferenceException("StateComponent: fields not initialized");
            }

            _stateMachine.Initialize(ref _idleState);
        }

        public void OnEnable()
        {
            _inputData.DataChanged += ChangeState;
            _collisionData.DataChanged += ChangeState;
            _physicData.DataChanged += ChangeState;
        }

        public void OnDisable()
        {
            _inputData.DataChanged -= ChangeState;
            _collisionData.DataChanged -= ChangeState;
            _physicData.DataChanged -= ChangeState;
        }

        public void FixedUpdate()
        {
            _stateMachine.CurrentState.FixedUpdate();
        }

        public void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        private void ChangeState()
        {
            if (_inputData.JumpButtonPressed && _collisionData.OnGround)
            {
                _stateMachine.ChangeState(ref _jumpStartState);
                return;
            }
            if (_physicData.Falling)
            {
                _stateMachine.ChangeState(ref _flyingState);
                return;
            }
            if (_inputData.WalkButtonPressed)
            {
                _stateMachine.ChangeState(ref _walkState);
                return;
            }
            if (_collisionData.OnGround)
            {
                _stateMachine.ChangeState(ref _idleState);
            }
        }

        private readonly StateMachine _stateMachine;
        private StateBase _idleState, _walkState, _jumpStartState, _flyingState;

        private readonly PlayerInputData _inputData;
        private readonly PlayerCollisionData _collisionData;
        private readonly PlayerPhysicData _physicData;
    }
}