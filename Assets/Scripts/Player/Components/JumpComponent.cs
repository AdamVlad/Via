using Assets.Scripts.Extensions;
using Assets.Scripts.Patterns;
using Assets.Scripts.Player.Components.Base;
using System;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public sealed class JumpComponent : ComponentBase
    {
        public JumpComponent(
            GameObject player,
            IEventBus<PlayerStates> eventBus,
            PlayerSettings settings) : base(eventBus)
        {
            _rigidbody = player.GetComponentOrThrowException<Rigidbody2D>();
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            _eventBus.Subscribe(PlayerStates.JumpStart, Jump);
        }

        protected override void DeactivateInternal()
        {
            _eventBus.Unsubscribe(PlayerStates.JumpStart, Jump);
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _rigidbody.mass * _settings.JumpForce);
        }

        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerSettings _settings;
    }
}