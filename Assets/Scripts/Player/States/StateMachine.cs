namespace Assets.Scripts.Player.States
{
    public class StateMachine
    {
        public StateBase CurrentState { get; private set; }

        public void Initialize(StateBase startState)
        {
            CurrentState = startState;
            CurrentState.Enter();
        }

        public void ChangeState(StateBase newState)
        {
            if (newState == CurrentState) return;

            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}

