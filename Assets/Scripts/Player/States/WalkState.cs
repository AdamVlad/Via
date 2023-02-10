using Assets.Scripts.Player.Data;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public sealed class WalkState : StateBase
    {
        public WalkState(
            GameObject character, 
            StateMachine stateMachine, 
            ref PlayerInputData inputData,
            ref PlayerCollisionData collisionData) : base(character, stateMachine)
        {
            _inputData = inputData;
            _collisionData = collisionData;

            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            if (_armature == null) return;

            _armature.armature.flipX = _inputData.LeftWalkButtonPressed;

            if (_collisionData.OnGround)
            {
                _armature.animation.FadeIn("Walk", 0.1f);
            }
        }

        public override void FixedUpdate()
        {
            _rigidbody?.AddForce(Vector2.right * _inputData.AxisXPressedValue * 20);
        }

        private UnityArmatureComponent _armature;
        private Rigidbody2D _rigidbody;

        private PlayerInputData _inputData;
        private PlayerCollisionData _collisionData;
    }
}

