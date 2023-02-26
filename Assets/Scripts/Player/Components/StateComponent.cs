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
                data => data.InputDataHashed.JumpButtonPressed);

            _fallState = new FallState(
                ref _stateMachine, 
                ref _eventBus,
                data => data.FallingDataHashed.IsFalling);

            _moveRightState = new MoveRightState(
                ref _stateMachine,
                ref _eventBus,
                data => data.InputDataHashed.MoveRightButtonPressed && data.GroundAndWallDataHashed.IsOnGround);

            _moveLeftState = new MoveLeftState(
                ref _stateMachine,
                ref _eventBus,
                data => data.InputDataHashed.MoveLeftButtonPressed && data.GroundAndWallDataHashed.IsOnGround);

            _moveLeftWhenFallingState = new MoveLeftWhenFallingState(
                ref _stateMachine,
                ref _eventBus,
                data => 
                    data.InputDataHashed.MoveLeftButtonPressed && !data.GroundAndWallDataHashed.IsOnGround);

            _moveRightWhenFallingState = new MoveRightWhenFallingState(
                ref _stateMachine,
                ref _eventBus,
                data => data.InputDataHashed.MoveRightButtonPressed && !data.GroundAndWallDataHashed.IsOnGround);

            _idleState.SetChild(ref _jumpStartState);
            _idleState.SetChild(ref _moveRightState);
            _idleState.SetChild(ref _moveLeftState);

            _jumpStartState.SetChild(ref _fallState);
            _jumpStartState.SetChild(ref _moveRightWhenFallingState);
            _jumpStartState.SetChild(ref _moveLeftWhenFallingState);

            _fallState.SetChild(ref _idleState);
            _fallState.SetChild(ref _moveLeftState);
            _fallState.SetChild(ref _moveRightState);
            _fallState.SetChild(ref _moveLeftWhenFallingState);
            _fallState.SetChild(ref _moveRightWhenFallingState);

            _moveRightState.SetChild(ref _idleState);
            _moveRightState.SetChild(ref _jumpStartState);
            _moveRightState.SetChild(ref _moveLeftState);

            _moveLeftState.SetChild(ref _idleState);
            _moveLeftState.SetChild(ref _jumpStartState);
            _moveLeftState.SetChild(ref _moveRightState);

            _moveLeftWhenFallingState.SetChild(ref _idleState);
            _moveLeftWhenFallingState.SetChild(ref _moveRightWhenFallingState);
            _moveLeftWhenFallingState.SetChild(ref _fallState);
            _moveLeftWhenFallingState.SetChild(ref _moveLeftState);
            _moveLeftWhenFallingState.SetChild(ref _moveRightState);

            _moveRightWhenFallingState.SetChild(ref _idleState);
            _moveRightWhenFallingState.SetChild(ref _moveLeftWhenFallingState);
            _moveRightWhenFallingState.SetChild(ref _fallState);
            _moveRightWhenFallingState.SetChild(ref _moveLeftState);
            _moveRightWhenFallingState.SetChild(ref _moveRightState);

            _stateMachine.Initialize(ref _idleState);
        }

        private DataComponent _dataComponent;

        private StateMachine _stateMachine;

        private StateNodeBase _idleState, _jumpStartState, _fallState, _moveRightState, _moveLeftState,
            _moveLeftWhenFallingState, _moveRightWhenFallingState;
    }


    //public sealed class StateComponent : ComponentBase, IObserver//, ILateTickable
    //{
    //    public StateComponent(
    //        IEventBus<PlayerEvents> eventBus,
    //        InputComponent inputComponent,
    //        GroundAndWallCheckerComponent groundAndWallCheckerObservable,
    //        FallTrackingComponent fallTrackingComponent,
    //        AnimationComponent animationComponent) : base(eventBus)
    //    {
    //        _inputObservable = inputComponent.IfNullThrowExceptionOrReturn();
    //        _groundAndWallCheckerObservable = groundAndWallCheckerObservable.IfNullThrowExceptionOrReturn();
    //        _fallObservable = fallTrackingComponent.IfNullThrowExceptionOrReturn();
    //        _animationObservable = animationComponent.IfNullThrowExceptionOrReturn();

    //        _inputDataHashed = new InputData();
    //        _groundAndWallDataHashed = new GroundAndWallCheckerData();
    //        _fallingDataHashed = new FallingData();

    //        _currentState = PlayerEvents.OnIdleStateEnter;
    //    }

    //    protected override void ActivateInternal()
    //    {
    //        base.ActivateInternal();

    //        _inputObservable.AddObserver(this);
    //        _groundAndWallCheckerObservable.AddObserver(this);
    //        _fallObservable.AddObserver(this);
    //        _animationObservable.AddObserver(this);
    //    }

    //    protected override void DeactivateInternal()
    //    {
    //        base.DeactivateInternal();

    //        _inputObservable.RemoveObserver(this);
    //        _groundAndWallCheckerObservable.RemoveObserver(this);
    //        _fallObservable.RemoveObserver(this);
    //        _animationObservable.RemoveObserver(this);
    //    }

    //    public void Update(ref IData data)
    //    {
    //        switch (data)
    //        {
    //            case InputData inputData:
    //                _inputDataHashed = inputData;
    //                break;
    //            case GroundAndWallCheckerData groundAndWallCheckerData:
    //                _groundAndWallDataHashed = groundAndWallCheckerData;
    //                break;
    //            case FallingData fallingData:
    //                _fallingDataHashed = fallingData;
    //                break;
    //        }

    //        _dirty = true;
    //    }

    //    //public void LateTick()
    //    //{
    //    //    if (_dirty)
    //    //    {
    //    //        ChangeState();
    //    //        _dirty = false;
    //    //    }
    //    //}

    //    //private void ChangeState()
    //    //{
    //    //    if (!_inputDataHashed.MoveRightButtonPressed &&
    //    //        !_inputDataHashed.MoveLeftButtonPressed ||
    //    //        _inputDataHashed.SimpleAttackButtonPressed &&
    //    //        _groundAndWallDataHashed.IsOnGround)
    //    //    {
    //    //        StopMove();
    //    //    }

    //    //    if (!_inputDataHashed.MoveBoostButtonPressed)
    //    //    {
    //    //        StopBoost();
    //    //    }

    //    //    if (_inputDataHashed.JumpButtonPressed &&
    //    //        _groundAndWallDataHashed.IsOnGround &&
    //    //        _currentState != PlayerEvents.OnJumpStartStateEnter &&
    //    //        _currentState != PlayerEvents.SimpleAttackStart &&
    //    //        _currentState != PlayerEvents.SimpleAttackEnd)
    //    //    {
    //    //        Jump();
    //    //        return;
    //    //    }

    //    //    if (_fallingDataHashed.IsFalling &&
    //    //        _currentState != PlayerEvents.MoveLeftBoost &&
    //    //        _currentState != PlayerEvents.MoveRightBoost &&
    //    //        _currentState != PlayerEvents.OnFallStateEnter &&
    //    //        _currentState != PlayerEvents.SimpleAttackStart &&
    //    //        _currentState != PlayerEvents.SimpleAttackEnd)
    //    //    {
    //    //        OnFallStateEnter();
    //    //        return;
    //    //    }

    //    //    if (_inputDataHashed.SimpleAttackButtonPressed &&
    //    //        _groundAndWallDataHashed.IsOnGround &&
    //    //        !_isLockToChangeState)
    //    //    {
    //    //        _isLockToChangeState = true;
    //    //        SimpleAttackStart();
    //    //        return;
    //    //    }

    //    //    if (!_inputDataHashed.SimpleAttackButtonPressed &&
    //    //        _currentState == PlayerEvents.SimpleAttackStart)
    //    //    {
    //    //        _isLockToChangeState = false;
    //    //        SimpleAttackEnd();
    //    //        return;
    //    //    }

    //    //    if (_inputDataHashed.MoveLeftButtonPressed &&
    //    //        _currentState != PlayerEvents.SimpleAttackStart &&
    //    //        _currentState != PlayerEvents.SimpleAttackEnd &&
    //    //        !_isLockToChangeState)
    //    //    {
    //    //        OnMoveLeftStateEnter();
    //    //        return;
    //    //    }

    //    //    if (_inputDataHashed.MoveRightButtonPressed &&
    //    //        _currentState != PlayerEvents.SimpleAttackStart &&
    //    //        _currentState != PlayerEvents.SimpleAttackEnd &&
    //    //        !_isLockToChangeState)
    //    //    {
    //    //        OnMoveRightStateEnter();
    //    //        return;
    //    //    }

    //    //    if (_groundAndWallDataHashed.IsOnGround &&
    //    //        _currentState != PlayerEvents.OnIdleStateEnter &&
    //    //        !_isLockToChangeState)
    //    //    {
    //    //        OnIdleStateEnter();
    //    //    }
    //    //}

    //    //private void StopMove()
    //    //{
    //    //    _eventBus.RaiseEvent(PlayerEvents.StopMove);
    //    //}

    //    //private void StopBoost()
    //    //{
    //    //    _eventBus.RaiseEvent(PlayerEvents.MoveBoostStopped);
    //    //}

    //    //private void Jump()
    //    //{
    //    //    _currentState = PlayerEvents.OnJumpStartStateEnter;
    //    //    _eventBus.RaiseEvent(_inputDataHashed.MoveBoostButtonPressed
    //    //        ? PlayerEvents.JumpStartWhenBoosted
    //    //        : PlayerEvents.OnJumpStartStateEnter);
    //    //}

    //    //private void OnFallStateEnter()
    //    //{
    //    //    _currentState = PlayerEvents.OnFallStateEnter;
    //    //    _eventBus.RaiseEvent(PlayerEvents.OnFallStateEnter);
    //    //}

    //    //private void OnMoveLeftStateEnter()
    //    //{
    //    //    if (_inputDataHashed.MoveBoostButtonPressed)
    //    //    {
    //    //        if (_currentState != PlayerEvents.MoveLeftBoost)
    //    //        {
    //    //            _currentState = PlayerEvents.MoveLeftBoost;
    //    //            _eventBus.RaiseEvent(PlayerEvents.MoveLeftBoost);
    //    //            _eventBus.RaiseEvent(PlayerEvents.OnMoveLeftWhenFallingStateEnter);
    //    //        }

    //    //        return;
    //    //    }

    //    //    _currentState = PlayerEvents.OnMoveLeftStateEnter;

    //    //    _eventBus.RaiseEvent(_groundAndWallDataHashed.IsOnGround
    //    //        ? PlayerEvents.OnMoveLeftStateEnter
    //    //        : PlayerEvents.OnMoveLeftWhenFallingStateEnter);
    //    //}

    //    //private void OnMoveRightStateEnter()
    //    //{
    //    //    if (_inputDataHashed.MoveBoostButtonPressed)
    //    //    {
    //    //        if (_currentState != PlayerEvents.MoveRightBoost)
    //    //        {
    //    //            _currentState = PlayerEvents.MoveRightBoost;
    //    //            _eventBus.RaiseEvent(PlayerEvents.MoveRightBoost);
    //    //            _eventBus.RaiseEvent(PlayerEvents.OnMoveRightWhenFallingStateEnter);
    //    //        }

    //    //        return;
    //    //    }

    //    //    _currentState = PlayerEvents.OnMoveRightStateEnter;

    //    //    _eventBus.RaiseEvent(_groundAndWallDataHashed.IsOnGround
    //    //        ? PlayerEvents.OnMoveRightStateEnter
    //    //        : PlayerEvents.OnMoveRightWhenFallingStateEnter);
    //    //}

    //    //private void SimpleAttackStart()
    //    //{
    //    //    _currentState = PlayerEvents.SimpleAttackStart;
    //    //    _eventBus.RaiseEvent(PlayerEvents.SimpleAttackStart);
    //    //}

    //    //private void SimpleAttackEnd()
    //    //{
    //    //    _currentState = PlayerEvents.SimpleAttackEnd;
    //    //    _eventBus.RaiseEvent(PlayerEvents.SimpleAttackEnd);
    //    //}

    //    //private void OnIdleStateEnter()
    //    //{
    //    //    _currentState = PlayerEvents.OnIdleStateEnter;
    //    //    _eventBus.RaiseEvent(PlayerEvents.OnIdleStateEnter);
    //    //}

    //    private readonly ObservableComponentDecorator _inputObservable;
    //    private readonly ObservableComponentDecorator _groundAndWallCheckerObservable;
    //    private readonly ObservableComponentDecorator _fallObservable;
    //    private readonly ObservableComponentDecorator _animationObservable;

    //    private InputData _inputDataHashed;
    //    private GroundAndWallCheckerData _groundAndWallDataHashed;
    //    private FallingData _fallingDataHashed;

    //    private bool _dirty;
    //    private bool _isLockToChangeState;

    //    private PlayerEvents _currentState;
    //}
}