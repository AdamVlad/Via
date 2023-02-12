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
            [Inject(Id = "PlayerFlyingState")] StateBase flyingState)
        {
            _stateMachine = stateMachine;

            _idleState = idleState;
            _walkState = walkState;
            _jumpStartState = jumpStartState;
            _flyingState = flyingState;
        }

        public void Start()
        {
            _stateMachine.Initialize(_idleState);
        }

        public void FixedUpdate(ref PlayerInputData inputData, ref PlayerCollisionData collisionData)
        {
            if (inputData.JumpButtonPressed && collisionData.OnGround)
            {
                _stateMachine.ChangeState(_jumpStartState);
            }
            if (collisionData.Flying)
            {
                _stateMachine.ChangeState(_flyingState);
            }
            if (inputData.WalkButtonPressed)
            {
                _stateMachine.ChangeState(_walkState);
            }
            else if (collisionData.OnGround)
            {
                _stateMachine.ChangeState(_idleState);
            }

            _stateMachine.CurrentState.FixedUpdate();
        }

        public void Update()
        {
            _stateMachine.CurrentState.Update();
        }

        private StateMachine _stateMachine;
        private StateBase _idleState, _walkState, _jumpStartState, _flyingState;
    }
}