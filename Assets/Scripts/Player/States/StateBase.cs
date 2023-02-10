using Assets.Scripts.Player.Settings;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public abstract class StateBase
    {
        protected StateBase(
            GameObject character,
            StateMachine stateMachine,
            PlayerSettings settings)
        {
            _character = character;
            _stateMachine = stateMachine;
            _settings = settings;
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
        protected PlayerSettings _settings;
    }
}

