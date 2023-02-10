using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Settings;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States.Concrete
{
    public sealed class WalkState : StateBase
    {
        public WalkState(
            GameObject character, 
            StateMachine stateMachine,
            PlayerSettings settings,
            ref PlayerInputData inputData,
            ref PlayerCollisionData collisionData) : base(character, stateMachine, settings)
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
                _armature.animation.FadeIn(_settings.WalkAnimationName, _settings.WalkStateTransition);
            }
        }

        public override void FixedUpdate()
        {
            _rigidbody?.AddForce(Vector2.right * _inputData.AxisXPressedValue * _settings.MaxSpeed);
        }

        private UnityArmatureComponent _armature;
        private Rigidbody2D _rigidbody;

        private PlayerInputData _inputData;
        private PlayerCollisionData _collisionData;
    }
}

