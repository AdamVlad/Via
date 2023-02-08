using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public abstract class StateBase
    {
        protected StateBase(GameObject character, StateMachine stateMachine)
        {
            _character = character;
            _stateMachine = stateMachine;
        }

        public virtual void Enter()
        {
        }

        public virtual void Exit()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        protected GameObject _character;
        protected StateMachine _stateMachine;
    }
}

