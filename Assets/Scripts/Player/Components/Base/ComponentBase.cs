using System;

using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components.Base
{
    public abstract class ComponentBase : IPlayerComponent
    {
        protected ComponentBase(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings)
        {
            _eventBus = eventBus ?? throw new NullReferenceException("IEventBus<PlayerEvents> is null");
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        public void Start(Player player)
        {
            _player = player.IfNullThrowExceptionOrReturn();

            StartInternal();
        }

        public void Activate()
        {
            ActivateInternal();
        }

        public void Deactivate()
        {
            DeactivateInternal();
        }

        protected virtual void StartInternal()
        {
        }

        protected virtual void ActivateInternal()
        {
        }

        protected virtual void DeactivateInternal()
        {
        }

        protected IEventBus<PlayerEvents> _eventBus;
        protected PlayerSettings _settings;
        protected Player _player;
    }
}