using UnityEngine;
using Zenject;

using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components
{
    public sealed class FallTrackingComponent : ObservableComponentDecorator, IFixedTickable
    {
        public FallTrackingComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
        }

        protected override void StartInternal()
        {
            base.StartInternal();

            _rigidbody = _player.gameObject.GetComponentInChildrenOrThrowException<Rigidbody2D>();
        }

        public void FixedTick()
        {
            var isFalling = _rigidbody.velocity.y < 0;

            if (!isFalling.Equals(_isFallingInPreviousMoment))
            {
                Notify(new FallingData
                {
                    IsFalling = isFalling
                });
            }

            _isFallingInPreviousMoment = isFalling;
        }

        private Rigidbody2D _rigidbody;

        private bool _isFallingInPreviousMoment;
    }
}