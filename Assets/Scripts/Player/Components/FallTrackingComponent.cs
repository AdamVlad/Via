using UnityEngine;
using Zenject;

using Assets.Scripts.Extensions;
using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;

namespace Assets.Scripts.Player.Components
{
    public sealed class FallTrackingComponent : ObservableComponentDecorator, IFixedTickable
    {
        public FallTrackingComponent(
            IEventBus<PlayerStates> eventBus,
            GameObject player) : base(eventBus)
        {
            _rigidbody = player.GetComponentOrThrowException<Rigidbody2D>();
        }

        public void FixedTick()
        {
            var isFalling = _rigidbody.velocity.y < 0;

            if (isFalling && !_isFallingInPreviousMoment)
            {
                Notify(new FallingData
                {
                    IsFalling = true
                });
            }
            if (!isFalling.Equals(_isFallingInPreviousMoment))
            {
                _isFallingInPreviousMoment = isFalling;
            }
        }

        private readonly Rigidbody2D _rigidbody;

        private bool _isFallingInPreviousMoment;
    }
}