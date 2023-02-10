using Assets.Scripts.Player.Data;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public sealed class JumpStartState : StateBase
    {
        public JumpStartState(GameObject character, StateMachine stateMachine, ref PlayerCollisionData collisionData) : base(character, stateMachine)
        {
            _collisionData = collisionData;

            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            _collisionData.OnGround = false;
            _armature?.animation.FadeIn("JumpStart", 0.1f, 1);
            _rigidbody?.AddForce(Vector2.up * 1000);
        }

        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (_armature.animation.isCompleted && !_collisionData.OnGround)
            {
                _collisionData.Flying = true;
            }
        }

        private UnityArmatureComponent _armature;
        private Rigidbody2D _rigidbody;

        private PlayerCollisionData _collisionData;
    }
}