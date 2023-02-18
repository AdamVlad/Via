using System;
using Assets.Scripts.Patterns;
using Assets.Scripts.Player.Components.Interfaces;

namespace Assets.Scripts.Player.Components.Base
{
    public abstract class ComponentBase : IPlayerComponent
    {
        protected ComponentBase(IEventBus<PlayerStates> eventBus)
        {
            _eventBus = eventBus ?? throw new NullReferenceException("IEventBus<PlayerStates> is null");
        }

        public void Activate()
        {
            ActivateInternal();
        }

        public void Deactivate()
        {
            DeactivateInternal();
        }

        protected virtual void ActivateInternal()
        {
        }

        protected virtual void DeactivateInternal()
        {
        }

        protected IEventBus<PlayerStates> _eventBus;
    }
}
