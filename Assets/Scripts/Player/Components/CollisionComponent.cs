using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public class CollisionComponent : ICollisionComponent
    {
        public CollisionComponent(PlayerCollisionData collisionData)
        {
            _collisionData = collisionData;
        }

        public void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _collisionData.OnGround = true;
            }
        }

        public void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Ground"))
            {
                _collisionData.OnGround = false;
                _collisionData.Flying = false;
            }
        }

        private PlayerCollisionData _collisionData;
    }
}

