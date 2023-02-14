using System;
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
            if (character == null)
            {
                throw new NullReferenceException("StateBase: GameObject not set");
            }
            if (settings == null)
            {
                throw new NullReferenceException("StateBase: PlayerSettings not set");
            }

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

