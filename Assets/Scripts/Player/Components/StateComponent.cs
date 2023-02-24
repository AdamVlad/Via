using Zenject;

using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Patterns.Observer;
using Assets.Scripts.Extensions;

namespace Assets.Scripts.Player.Components
{
    public sealed class StateComponent : ComponentBase, IObserver, ILateTickable
    {
        public StateComponent(
            IEventBus<PlayerStates> eventBus,
            InputComponent inputComponent,
            GroundAndWallCheckerComponent groundAndWallCheckerObservable,
            FallTrackingComponent fallTrackingComponent) : base(eventBus)
        {
            _inputObservable = inputComponent.IfNullThrowExceptionOrReturn();
            _groundAndWallCheckerObservable = groundAndWallCheckerObservable.IfNullThrowExceptionOrReturn();
            _fallObservable = fallTrackingComponent.IfNullThrowExceptionOrReturn();

            _inputDataHashed = new InputData();
            _groundAndWallDataHashed = new GroundAndWallCheckerData();
            _fallingDataHashed = new FallingData();

            _currentState = PlayerStates.Idle;
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _inputObservable.AddObserver(this);
            _groundAndWallCheckerObservable.AddObserver(this);
            _fallObservable.AddObserver(this);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _inputObservable.RemoveObserver(this);
            _groundAndWallCheckerObservable.RemoveObserver(this);
            _fallObservable.RemoveObserver(this);
        }

        public void Update(ref IData data)
        {
            switch (data)
            {
                case InputData inputData:
                    _inputDataHashed = inputData;
                    break;
                case GroundAndWallCheckerData groundAndWallCheckerData:
                    _groundAndWallDataHashed = groundAndWallCheckerData;
                    break;
                case FallingData fallingData:
                    _fallingDataHashed = fallingData;
                    break;
            }

            _dirty = true;
        }

        public void LateTick()
        {
            if (_dirty)
            {
                ChangeState();
                _dirty = false;
            }
        }

        private void ChangeState()
        {
            if (!_inputDataHashed.MoveRightButtonPressed &&
                !_inputDataHashed.MoveLeftButtonPressed)
            {
                _eventBus.RaiseEvent(PlayerStates.MoveStopped);
            }

            if (_inputDataHashed.JumpButtonPressed && 
                _groundAndWallDataHashed.IsOnGround &&
                _currentState != PlayerStates.JumpStart)
            {
                _currentState = PlayerStates.JumpStart;
                _eventBus.RaiseEvent(PlayerStates.JumpStart);
                return;
            }

            if (_fallingDataHashed.IsFalling &&
                _currentState != PlayerStates.MoveBoost &&
                _currentState != PlayerStates.Fall)
            {
                _currentState = PlayerStates.Fall;
                _eventBus.RaiseEvent(PlayerStates.Fall);
                return;
            }

            if (_inputDataHashed.MoveLeftButtonPressed)
            {
                _currentState = PlayerStates.MoveLeft;

                if (_inputDataHashed.MoveBoostButtonPressed)
                {
                    _currentState = PlayerStates.MoveBoost;
                    _eventBus.RaiseEvent(PlayerStates.MoveBoost);
                    _eventBus.RaiseEvent(PlayerStates.MoveLeftWhenFlying);
                    return;
                }

                _eventBus.RaiseEvent(PlayerStates.MoveBoostStopped);
                _eventBus.RaiseEvent(_groundAndWallDataHashed.IsOnGround
                    ? PlayerStates.MoveLeft
                    : PlayerStates.MoveLeftWhenFlying);
                return;
            }

            if (_inputDataHashed.MoveRightButtonPressed)
            {
                _currentState = PlayerStates.MoveRight;

                if (_inputDataHashed.MoveBoostButtonPressed)
                {
                    _currentState = PlayerStates.MoveBoost;
                    _eventBus.RaiseEvent(PlayerStates.MoveBoost);
                    _eventBus.RaiseEvent(PlayerStates.MoveRightWhenFlying);
                    return;
                }

                _eventBus.RaiseEvent(PlayerStates.MoveBoostStopped);
                _eventBus.RaiseEvent(_groundAndWallDataHashed.IsOnGround
                    ? PlayerStates.MoveRight
                    : PlayerStates.MoveRightWhenFlying);
                return;
            }

            if (_groundAndWallDataHashed.IsOnGround && _currentState != PlayerStates.Idle)
            {
                _currentState = PlayerStates.Idle;
                _eventBus.RaiseEvent(PlayerStates.Idle);
            }
        }

        private readonly ObservableComponentDecorator _inputObservable;
        private readonly ObservableComponentDecorator _groundAndWallCheckerObservable;
        private readonly ObservableComponentDecorator _fallObservable;

        private InputData _inputDataHashed;
        private GroundAndWallCheckerData _groundAndWallDataHashed;
        private FallingData _fallingDataHashed;

        private bool _dirty;

        private PlayerStates _currentState;
    }
}