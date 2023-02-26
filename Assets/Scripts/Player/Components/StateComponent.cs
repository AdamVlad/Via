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
            _idleState = new IdleState(
                ref _stateMachine,
                ref _eventBus,
                data =>
                    data.GroundAndWallDataHashed.IsOnGround &&
                    !data.InputDataHashed.MoveRightButtonPressed &&
                    !data.InputDataHashed.MoveLeftButtonPressed);

            _jumpStartState = new JumpStartState(
                ref _stateMachine,
                ref _eventBus,
                data =>
                    data.InputDataHashed.JumpButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed);

            _jumpStartWhenBoostedState = new JumpStartWhenBoostedState(
                ref _stateMachine,
                ref _eventBus,
                data =>
                    data.InputDataHashed.JumpButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed &&
                    data.GroundAndWallDataHashed.IsOnGround);

            _fallState = new FallState(
                ref _stateMachine,
                ref _eventBus,
                data =>
                    data.FallingDataHashed.IsFalling &&
                    !(data.InputDataHashed.MoveBoostButtonPressed && data.InputDataHashed.MoveLeftButtonPressed ||
                      data.InputDataHashed.MoveBoostButtonPressed && data.InputDataHashed.MoveRightButtonPressed));

            _moveRightState = new MoveRightState(
                ref _stateMachine,
                ref _eventBus,
                data => data.InputDataHashed.MoveRightButtonPressed &&
                        !data.InputDataHashed.MoveBoostButtonPressed &&
                        data.GroundAndWallDataHashed.IsOnGround);

            _moveLeftState = new MoveLeftState(
                ref _stateMachine,
                ref _eventBus,
                data =>
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed &&
                    data.GroundAndWallDataHashed.IsOnGround);

            _moveLeftWhenFallingState = new MoveLeftWhenFallingState(
                ref _stateMachine,
                ref _eventBus,
                data =>
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    !data.InputDataHashed.MoveBoostButtonPressed &&
                    !data.GroundAndWallDataHashed.IsOnGround);

            _moveRightWhenFallingState = new MoveRightWhenFallingState(
                ref _stateMachine,
                ref _eventBus,
                data => data.InputDataHashed.MoveRightButtonPressed &&
                        !data.InputDataHashed.MoveBoostButtonPressed &&
                        !data.GroundAndWallDataHashed.IsOnGround);

            _boostedMoveLeftState = new BoostedMoveLeftState(
                ref _stateMachine,
                ref _eventBus,
                data =>
                    data.InputDataHashed.MoveLeftButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed);

            _boostedMoveRightState = new BoostedMoveRightState(
                ref _stateMachine,
                ref _eventBus,
                data =>
                    data.InputDataHashed.MoveRightButtonPressed &&
                    data.InputDataHashed.MoveBoostButtonPressed);
        }

        private void ConfigureStatesLinks()
        {
            _idleState.SetLinks(
                _jumpStartState,
                _moveRightState, 
                _moveLeftState,
                _boostedMoveLeftState,
                _boostedMoveRightState);

            _jumpStartState.SetLinks(
                _fallState, 
                _moveRightWhenFallingState, 
                _moveLeftWhenFallingState,
                _boostedMoveLeftState, 
                _boostedMoveRightState);

            _jumpStartWhenBoostedState.SetLinks(
                _fallState,
                _moveRightWhenFallingState,
                _moveLeftWhenFallingState,
                _boostedMoveLeftState,
                _boostedMoveRightState);

            _fallState.SetLinks(
                _idleState,
                _moveLeftState,
                _moveRightState, 
                _moveLeftWhenFallingState, 
                _moveRightWhenFallingState,
                _boostedMoveLeftState, 
                _boostedMoveRightState);

            _moveRightState.SetLinks(
                _idleState,
                _jumpStartState,
                _moveLeftState, 
                _boostedMoveLeftState,
                _boostedMoveRightState);

            _moveLeftState.SetLinks(
                _idleState,
                _jumpStartState, 
                _moveRightState, 
                _boostedMoveLeftState,
                _boostedMoveRightState);

            _moveLeftWhenFallingState.SetLinks(
                _idleState,
                _moveRightWhenFallingState,
                _fallState,
                _moveLeftState,
                _moveRightState, 
                _boostedMoveLeftState, 
                _boostedMoveRightState);

            _moveRightWhenFallingState.SetLinks(
                _idleState, 
                _moveLeftWhenFallingState,
                _fallState, 
                _moveLeftState,
                _moveRightState, 
                _boostedMoveLeftState, 
                _boostedMoveRightState);

            _boostedMoveLeftState.SetLinks(
                _idleState,
                _jumpStartWhenBoostedState,
                _fallState,
                _moveRightState, 
                _moveLeftState,
                _moveLeftWhenFallingState,
                _moveRightWhenFallingState,
                _boostedMoveRightState);

            _boostedMoveRightState.SetLinks(
                _idleState,
                _jumpStartWhenBoostedState,
                _fallState,
                _moveRightState, 
                _moveLeftState,
                _moveLeftWhenFallingState, 
                _moveRightWhenFallingState,
                _boostedMoveLeftState);
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
            _boostedMoveRightState;
    }
}