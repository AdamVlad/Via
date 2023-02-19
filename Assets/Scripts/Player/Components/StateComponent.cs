using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Patterns.Observer;
using Zenject;

namespace Assets.Scripts.Player.Components
{
    public sealed class StateComponent : ComponentBase, IObserver, ILateTickable
    {
        public StateComponent(
            IEventBus<PlayerStates> eventBus,
            InputComponent inputComponent,
            GroundAndWallCheckerComponent groundAndWallCheckerObservable) : base(eventBus)
        {
            _inputObservable = inputComponent;
            _groundAndWallCheckerObservable = groundAndWallCheckerObservable;

            _inputDataHashed = new InputData();
            _groundAndWallDataHashed = new GroundAndWallCheckerData();
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _inputObservable.AddObserver(this);
            _groundAndWallCheckerObservable.AddObserver(this);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _inputObservable.RemoveObserver(this);
            _groundAndWallCheckerObservable.RemoveObserver(this);
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
            if (_inputDataHashed.JumpButtonPressed && _groundAndWallDataHashed.IsOnGround)
            {
                _eventBus.RaiseEvent(PlayerStates.JumpStart);
                return;
            }
            if (_inputDataHashed.MoveLeftButtonPressed)
            {
                _eventBus.RaiseEvent(PlayerStates.MoveLeft);
                return;
            }
            if (_inputDataHashed.MoveRightButtonPressed)
            {
                _eventBus.RaiseEvent(PlayerStates.MoveRight);
                return;
            }
            if (_groundAndWallDataHashed.IsOnGround)
            {
                _eventBus.RaiseEvent(PlayerStates.Idle);
            }
        }

        private readonly ObservableComponentDecorator _inputObservable;
        private readonly ObservableComponentDecorator _groundAndWallCheckerObservable;

        private InputData _inputDataHashed;
        private GroundAndWallCheckerData _groundAndWallDataHashed;

        private bool _dirty;
    }
}