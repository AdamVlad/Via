using System;

using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Interfaces;

namespace Assets.Scripts.Player.Components.Base
{
    public abstract class ComponentBase : IPlayerComponent
    {
        protected ComponentBase(IEventBus<PlayerEvents> eventBus)
        {
            _eventBus = eventBus ?? throw new NullReferenceException("IEventBus<PlayerEvents> is null");
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

        protected IEventBus<PlayerEvents> _eventBus;
    }
}
