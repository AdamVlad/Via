using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Patterns.Observer;
using Assets.Scripts.Extensions;
using Assets.Scripts.Player.ComponentsData.Interfaces;
using Assets.Scripts.Player.States;
using Assets.Scripts.Player.States.Base;

namespace Assets.Scripts.Player.Components
{
    public sealed class StateComponent : ComponentBase, IObserver
    {
        public StateComponent(
            IEventBus<PlayerEvents> eventBus,
            DataComponent dataComponent) : base(eventBus)
        {
            _dataComponent = dataComponent.IfNullThrowExceptionOrReturn();

            ConstructStates();
        }

        private DataComponent _dataComponent;

        private StateMachine _stateMachine;

        private StateNodeBase
            _idleState,
            _jumpStartState,
            _jumpStartWhenBoostedState,
            _fallState,
            _moveRightState,
            _moveLeftState,
            _moveLeftWhenFallingState,
            _moveRightWhenFallingState,
            _boostedMoveLeftState,
            _boostedMoveRightState,
            _simpleAttackStartState,
            _simpleAttackEndState;

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _dataComponent.AddObserver(this);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _dataComponent.RemoveObserver(this);
        }

        public void Update(ref IData data)
        {
            _stateMachine.ChangeState();
        }

        private void ConstructStates()
        {
            _stateMachine = new StateMachine(ref _dataComponent);
            
            InitializeStates();

            ConfigureStatesLinks();

            _stateMachine.Initialize(ref _idleState);
        }

        private void InitializeStates()
        {
            _idleState = new IdleState(ref _stateMachine, ref _eventBus);
            _jumpStartState = new JumpStartState(ref _stateMachine, ref _eventBus);
            _jumpStartWhenBoostedState = new JumpStartWhenBoostedState(ref _stateMachine, ref _eventBus);
            _fallState = new FallState(ref _stateMachine, ref _eventBus);
            _moveRightState = new MoveRightState(ref _stateMachine, ref _eventBus);
            _moveLeftState = new MoveLeftState(ref _stateMachine, ref _eventBus);
            _moveLeftWhenFallingState = new MoveLeftWhenFallingState(ref _stateMachine, ref _eventBus);
            _moveRightWhenFallingState = new MoveRightWhenFallingState(ref _stateMachine, ref _eventBus);
            _boostedMoveLeftState = new BoostedMoveLeftState(ref _stateMachine, ref _eventBus);
            _boostedMoveRightState = new BoostedMoveRightState(ref _stateMachine, ref _eventBus);
            _simpleAttackStartState = new SimpleAttackStartState(ref _stateMachine, ref _eventBus);
            _simpleAttackEndState = new SimpleAttackEndState(ref _stateMachine, ref _eventBus);
        }

        private void ConfigureStatesLinks()
        {
            SetLinksForIdleState();
            SetLinksForSimpleAttackStartState();
            SetLinksForSimpleAttackEndState();
            SetLinksForJumpStartState();
            SetLinksForJumpStartWhenBoostedState();
            SetLinksForFallState();
            SetLinksForMoveRightState();
            SetLinksForMoveLeftState();
            SetLinksForMoveLeftWhenFallingState();
            SetLinksForMoveRightWhenFallingState();
            SetLinksForBoostedMoveLeftState();
            SetLinksForBoostedMoveRightState();
        }

        private void SetLinksForIdleState()
        {
            _idleState.SetLink(
                ref _jumpStartState, 
                data =>
                    data.InputDataHashed.JumpButtonPressed);

            _idleState.SetLink(
                ref _moveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _idleState.SetLink(
                ref _moveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _idleState.SetLink(
                ref _boostedMoveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed);

            _idleState.SetLink(
                ref _boostedMoveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _idleState.SetLink(
                ref _simpleAttackStartState,
                data => 
                    data.InputDataHashed.SimpleAttackButtonPressed);
        }

        private void SetLinksForSimpleAttackStartState()
        {
            _simpleAttackStartState.SetLink(
                ref _simpleAttackEndState,
                data => 
                    !data.InputDataHashed.SimpleAttackButtonPressed);
        }

        private void SetLinksForSimpleAttackEndState()
        {
            _simpleAttackEndState.SetLink(
                ref _idleState,
                data =>
                    data.AttackDataHashed.IsAttackEnded &&
                    !data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.JumpButtonPressed);

            _simpleAttackEndState.SetLink(
                ref _moveLeftState,
                data =>
                    data.AttackDataHashed.IsAttackEnded &&
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _simpleAttackEndState.SetLink(
                ref _moveRightState,
                data =>
                    data.AttackDataHashed.IsAttackEnded &&
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _simpleAttackEndState.SetLink(
                ref _boostedMoveLeftState,
                data =>
                    data.AttackDataHashed.IsAttackEnded &&
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed);

            _simpleAttackEndState.SetLink(
                ref _boostedMoveRightState,
                data =>
                    data.AttackDataHashed.IsAttackEnded &&
                    data.InputDataHashed.MoveRightButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed);
        }

        private void SetLinksForJumpStartState()
        {
            _jumpStartState.SetLink(
                ref _fallState, 
                data => 
                    data.FallingDataHashed.IsFalling);

            _jumpStartState.SetLink(
                ref _moveRightWhenFallingState,
                data =>
                    data.InputDataHashed.MoveRightButtonPressed && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _jumpStartState.SetLink(
                ref _moveLeftWhenFallingState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _jumpStartState.SetLink(
                ref _boostedMoveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _jumpStartState.SetLink(
                ref _boostedMoveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);
        }

        private void SetLinksForJumpStartWhenBoostedState()
        {
            _jumpStartWhenBoostedState.SetLink(
                ref _fallState,
                data =>
                    data.FallingDataHashed.IsFalling && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _jumpStartWhenBoostedState.SetLink(
                ref _moveRightWhenFallingState,
                data =>
                    data.InputDataHashed.MoveRightButtonPressed && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _jumpStartWhenBoostedState.SetLink(
                ref _moveLeftWhenFallingState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _jumpStartWhenBoostedState.SetLink(
                ref _boostedMoveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _jumpStartWhenBoostedState.SetLink(
                ref _boostedMoveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);
        }

        private void SetLinksForFallState()
        {
            _fallState.SetLink(
                ref _idleState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    !data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveRightButtonPressed);

            _fallState.SetLink(
                ref _moveLeftState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _fallState.SetLink(
                ref _moveRightState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _fallState.SetLink(
                ref _moveLeftWhenFallingState,
                data => 
                    !data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _fallState.SetLink(
                ref _moveRightWhenFallingState,
                data => 
                    !data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _fallState.SetLink(
                ref _boostedMoveLeftState,
                data =>
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _fallState.SetLink(
                ref _boostedMoveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);
        }

        private void SetLinksForMoveRightState()
        {
            _moveRightState.SetLink(
                ref _idleState,
                data => 
                    !data.InputDataHashed.MoveLeftButtonPressed && 
                    !data.InputDataHashed.MoveRightButtonPressed);

            _moveRightState.SetLink(
                ref _jumpStartState, 
                data =>
                    data.InputDataHashed.JumpButtonPressed);

            _moveRightState.SetLink(
                ref _moveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _moveRightState.SetLink(
                ref _boostedMoveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed);

            _moveRightState.SetLink(
                ref _boostedMoveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _moveRightState.SetLink(
                ref _simpleAttackStartState,
                data =>
                    data.InputDataHashed.SimpleAttackButtonPressed);
        }

        private void SetLinksForMoveLeftState()
        {
            _moveLeftState.SetLink(
                ref _idleState,
                data => 
                    !data.InputDataHashed.MoveLeftButtonPressed && 
                    !data.InputDataHashed.MoveRightButtonPressed);

            _moveLeftState.SetLink(
                ref _jumpStartState, 
                data => 
                    data.InputDataHashed.JumpButtonPressed);

            _moveLeftState.SetLink(
                ref _moveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _moveLeftState.SetLink(
                ref _boostedMoveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _moveLeftState.SetLink(
                ref _boostedMoveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _moveLeftState.SetLink(
                ref _simpleAttackStartState,
                data =>
                    data.InputDataHashed.SimpleAttackButtonPressed);
        }

        private void SetLinksForMoveLeftWhenFallingState()
        {
            _moveLeftWhenFallingState.SetLink(
                ref _idleState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    !data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveRightButtonPressed);

            _moveLeftWhenFallingState.SetLink(
                ref _moveRightWhenFallingState,
                data => 
                    !data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _moveLeftWhenFallingState.SetLink(
                ref _moveLeftState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _moveLeftWhenFallingState.SetLink(
                ref _moveRightState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _moveLeftWhenFallingState.SetLink(
                ref _boostedMoveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _moveLeftWhenFallingState.SetLink(
                ref _boostedMoveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _moveLeftWhenFallingState.SetLink(
                ref _fallState,
                data => 
                    data.FallingDataHashed.IsFalling && 
                    !data.InputDataHashed.MoveBoostButtonPressed);
        }

        private void SetLinksForMoveRightWhenFallingState()
        {
            _moveRightWhenFallingState.SetLink(
                ref _idleState,
                data =>
                    data.GroundAndWallDataHashed.IsOnGround && 
                    !data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveRightButtonPressed);

            _moveRightWhenFallingState.SetLink(
                ref _moveLeftWhenFallingState,
                data => 
                    !data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _moveRightWhenFallingState.SetLink(
                ref _moveLeftState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _moveRightWhenFallingState.SetLink(
                ref _moveRightState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _moveRightWhenFallingState.SetLink(
                ref _boostedMoveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _moveRightWhenFallingState.SetLink(
                ref _boostedMoveRightState,
                data => 
                    data.InputDataHashed.MoveRightButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _moveRightWhenFallingState.SetLink(
                ref _fallState,
                data => 
                    data.FallingDataHashed.IsFalling && 
                    !data.InputDataHashed.MoveBoostButtonPressed);
        }

        private void SetLinksForBoostedMoveLeftState()
        {
            _boostedMoveLeftState.SetLink(
                ref _idleState,
                data =>
                    data.GroundAndWallDataHashed.IsOnGround && 
                    !data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveRightButtonPressed);

            _boostedMoveLeftState.SetLink(
                ref _jumpStartWhenBoostedState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.JumpButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveLeftState.SetLink(
                ref _moveLeftState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveLeftState.SetLink(
                ref _moveRightState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveLeftState.SetLink(
                ref _moveLeftWhenFallingState,
                data => 
                    !data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveLeftState.SetLink(
                ref _moveRightWhenFallingState,
                data => 
                    !data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveLeftState.SetLink(
                ref _fallState,
                data =>
                    data.FallingDataHashed.IsFalling && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveLeftState.SetLink(
                ref _boostedMoveRightState,
                data =>
                    data.InputDataHashed.MoveRightButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveLeftState.SetLink(
                ref _simpleAttackStartState,
                data =>
                    data.InputDataHashed.SimpleAttackButtonPressed &&
                    data.GroundAndWallDataHashed.IsOnGround);
        }

        private void SetLinksForBoostedMoveRightState()
        {
            _boostedMoveRightState.SetLink(
                ref _idleState,
                data =>
                    data.GroundAndWallDataHashed.IsOnGround && 
                    !data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveRightButtonPressed);

            _boostedMoveRightState.SetLink(
                ref _jumpStartWhenBoostedState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.JumpButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveRightState.SetLink(
                ref _moveLeftState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveRightState.SetLink(
                ref _moveRightState,
                data => 
                    data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveRightState.SetLink(
                ref _moveLeftWhenFallingState,
                data => 
                    !data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveRightState.SetLink(
                ref _moveRightWhenFallingState,
                data => 
                    !data.GroundAndWallDataHashed.IsOnGround && 
                    data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveRightState.SetLink(
                ref _fallState,
                data => 
                    data.FallingDataHashed.IsFalling && 
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveRightState.SetLink(
                ref _boostedMoveLeftState,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && 
                    data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveRightState.SetLink(
                ref _simpleAttackStartState,
                data =>
                    data.InputDataHashed.SimpleAttackButtonPressed &&
                    data.GroundAndWallDataHashed.IsOnGround);
        }
    }
}