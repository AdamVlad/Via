using Assets.Scripts.Player.Settings;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public abstract class StateBase
    {
        protected StateBase(
            GameObject character,
            PlayerSettings settings)
        {
            _character = character;
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
        protected PlayerSettings _settings;
    }
}

