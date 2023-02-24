using Zenject;

using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Patterns.Observer;
using Assets.Scripts.Extensions;
using Assets.Scripts.Player.ComponentsData.Interfaces;

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
                StopMove();
            }

            if (!_inputDataHashed.MoveBoostButtonPressed)
            {
                StopBoost();
            }

            if (_inputDataHashed.JumpButtonPressed && 
                _groundAndWallDataHashed.IsOnGround &&
                _currentState != PlayerStates.JumpStart)
            {
                Jump();
                return;
            }

            if (_fallingDataHashed.IsFalling &&
                _currentState != PlayerStates.MoveLeftBoost &&
                _currentState != PlayerStates.MoveRightBoost &&
                _currentState != PlayerStates.Fall &&
                _currentState != PlayerStates.SimpleAttack)
            {
                Fall();
                return;
            }

            if (_inputDataHashed.MoveLeftButtonPressed)
            {
                MoveLeft();
                return;
            }

            if (_inputDataHashed.MoveRightButtonPressed)
            {
                MoveRight();
                return;
            }

            if (_inputDataHashed.SimpleAttackButtonPressed &&
                _currentState != PlayerStates.JumpStart &&
                _currentState != PlayerStates.Fall &&
                _groundAndWallDataHashed.IsOnGround)
            {
                SimpleAttack();
                return;
            }

            if (_groundAndWallDataHashed.IsOnGround &&
                _currentState != PlayerStates.Idle)
            {
                Idle();
            }
        }

        private void StopMove()
        {
            _eventBus.RaiseEvent(PlayerStates.MoveStopped);
        }

        private void StopBoost()
        {
            _eventBus.RaiseEvent(PlayerStates.MoveBoostStopped);
        }

        private void Jump()
        {
            _currentState = PlayerStates.JumpStart;
            _eventBus.RaiseEvent(_inputDataHashed.MoveBoostButtonPressed
                ? PlayerStates.JumpStartWhenBoosted
                : PlayerStates.JumpStart);
        }

        private void Fall()
        {
            _currentState = PlayerStates.Fall;
            _eventBus.RaiseEvent(PlayerStates.Fall);
        }

        private void MoveLeft()
        {
            if (_inputDataHashed.MoveBoostButtonPressed)
            {
                if (_currentState != PlayerStates.MoveLeftBoost)
                {
                    _currentState = PlayerStates.MoveLeftBoost;
                    _eventBus.RaiseEvent(PlayerStates.MoveLeftBoost);
                    _eventBus.RaiseEvent(PlayerStates.MoveLeftWhenFlying);
                }
                return;
            }

            _currentState = PlayerStates.MoveLeft;

            _eventBus.RaiseEvent(_groundAndWallDataHashed.IsOnGround
                ? PlayerStates.MoveLeft
                : PlayerStates.MoveLeftWhenFlying);
        }

        private void MoveRight()
        {
            if (_inputDataHashed.MoveBoostButtonPressed)
            {
                if (_currentState != PlayerStates.MoveRightBoost)
                {
                    _currentState = PlayerStates.MoveRightBoost;
                    _eventBus.RaiseEvent(PlayerStates.MoveRightBoost);
                    _eventBus.RaiseEvent(PlayerStates.MoveRightWhenFlying);
                }

                return;
            }

            _currentState = PlayerStates.MoveRight;

            _eventBus.RaiseEvent(_groundAndWallDataHashed.IsOnGround
                ? PlayerStates.MoveRight
                : PlayerStates.MoveRightWhenFlying);
        }

        private void SimpleAttack()
        {
            _currentState = PlayerStates.SimpleAttack;
            _eventBus.RaiseEvent(PlayerStates.SimpleAttack);
        }

        private void Idle()
        {
            _currentState = PlayerStates.Idle;
            _eventBus.RaiseEvent(PlayerStates.Idle);
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