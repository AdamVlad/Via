using Assets.Scripts.Player.Data;
using System;
using Assets.Scripts.Player.Components.Interfaces;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public class PhysicComponent : IPhysicComponent
    {
        public PhysicComponent(
            GameObject player,
            PlayerPhysicData physicData)
        {
            if (!player.TryGetComponent(out _rigidbody))
            {
                throw new NullReferenceException("PhysicComponent: Rigidbody2D component not set on player");
            }

            _physicData = physicData;
        }

        public void FixedUpdate()
        {
            _physicData.Falling = _rigidbody.velocity.y < 0;
        }

        private readonly Rigidbody2D _rigidbody;
        private readonly PlayerPhysicData _physicData;
    }
}

