using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;

namespace Assets.Scripts.Player.Components
{
    public class StateComponent : IStateComponent
    {
        public StateComponent(
            IEventBus<PLayerStates> eventBus,
            InputData inputData,
            CollisionData collisionData,
            PhysicData physicData)
        {
            _eventBus = eventBus;

            _inputData = inputData;
            _collisionData = collisionData;
            _physicData = physicData;
        }

        public void OnEnable()
        {
            _inputData.DataChanged += ChangeState;
            _physicData.DataChanged += ChangeState;
        }

        public void OnDisable()
        {
            _inputData.DataChanged -= ChangeState;
            _physicData.DataChanged -= ChangeState;
        }

        private void ChangeState()
        {
            if (_inputData.MoveRightButtonPressed && !_lookRight)
            {
                _lookRight = true;
                _eventBus.RaiseEvent(PLayerStates.Flip);
            }
            if (_inputData.MoveLeftButtonPressed && _lookRight)
            {
                _lookRight = true;
                _eventBus.RaiseEvent(PLayerStates.Flip);
            }

            if (_inputData.JumpButtonPressed && _collisionData.OnGround)
            {
                _eventBus.RaiseEvent(PLayerStates.JumpStart);
                return;
            }
            if (_physicData.Falling)
            {
                _eventBus.RaiseEvent(PLayerStates.Fly);
                return;
            }
            if (_inputData.MoveLeftButtonPressed)
            {
                _eventBus.RaiseEvent(PLayerStates.MoveLeft);
                return;
            }
            if (_inputData.MoveRightButtonPressed)
            {
                _eventBus.RaiseEvent(PLayerStates.MoveRight);
                return;
            }
            if (_collisionData.OnGround)
            {
                _eventBus.RaiseEvent(PLayerStates.Idle);
            }
        }

        private readonly IEventBus<PLayerStates> _eventBus;

        private readonly InputData _inputData;
        private readonly CollisionData _collisionData;
        private readonly PhysicData _physicData;

        private bool _lookRight = true;
    }
}