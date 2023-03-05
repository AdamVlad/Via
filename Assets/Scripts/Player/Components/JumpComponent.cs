using UnityEngine;

using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components
{
    public sealed class JumpComponent : ComponentBase
    {
        public JumpComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
        }

        protected override void StartInternal()
        {
            base.StartInternal();

            _rigidbody = _player.gameObject.GetComponentInChildrenOrThrowException<Rigidbody2D>();
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnJumpStartStateEnter, Jump);
            _eventBus.Subscribe(PlayerEvents.OnJumpStartWhenBoostedStateEnter, Jump);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnJumpStartStateEnter, Jump);
            _eventBus.Unsubscribe(PlayerEvents.OnJumpStartWhenBoostedStateEnter, Jump);
        }

        public void Jump()
        {
            _rigidbody.AddForce(Vector2.up * _rigidbody.mass * _settings.JumpForce);
        }

        private Rigidbody2D _rigidbody;
    }
}