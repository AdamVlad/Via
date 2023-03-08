using Assets.Scripts.Player.Components;
using Assets.Scripts.Player.States.Base;

namespace Assets.Scripts.Player.States
{
    public class StateMachine
    {
        public StateMachine(ref DataComponent data)
        {
            _data = data;
        }

        public void Initialize(ref StateNodeBase startState)
        {
            CurrentState = startState;
            startState.Enter();
        }

        public StateNodeBase CurrentState { get; set; }

        public void ChangeState()
        {
            lock (_locker)
            {
                CurrentState.EnterNextState(ref _data);
            }
        }

        private DataComponent _data;
        private object _locker = new object();
    }
}