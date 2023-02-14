namespace Assets.Scripts.Player.States
{
    public class StateMachine
    {
        public StateBase CurrentState { get; private set; }

        public void Initialize(ref StateBase startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(ref StateBase newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}

