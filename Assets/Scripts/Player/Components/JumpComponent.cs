using System;
using UnityEngine;

using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Patterns.EventBus;

namespace Assets.Scripts.Player.Components
{
    public sealed class JumpComponent : ComponentBase
    {
        public JumpComponent(
            IEventBus<PlayerEvents> eventBus,
            GameObject player,
            PlayerSettings settings) : base(eventBus)
        {
            _rigidbody = player.GetComponentOrThrowException<Rigidbody2D>();
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnJumpStartStateEnter, Jump);
            _eventBus.Subscribe(PlayerEvents.JumpStartWhenBoosted, Jump);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnJumpStartStateEnter, Jump);
            _eventBus.Unsubscribe(PlayerEvents.JumpStartWhenBoosted, Jump);
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _rigidbody.mass * _settings.JumpForce);
        }

        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerSettings _settings;
    }
}