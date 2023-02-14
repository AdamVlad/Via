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
            PlayerSettings settings,
            PlayerInputData inputData,
            PlayerCollisionData collisionData) : base(character, settings)
        {
            _collisionData = collisionData;
            _inputData = inputData;

            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            if (_armature == null) return;

            _armature.armature.flipX = _inputData.WalkLeftButtonPressed;

            if (_collisionData.OnGround)
            {
                _armature.animation.FadeIn(_settings.WalkAnimationName, _settings.WalkStateTransition);
            }
        }

        public override void FixedUpdate()
        {
            _rigidbody.AddForce(Vector2.right * _inputData.AxisXPressedValue * _rigidbody.mass * _settings.MaxSpeed);

            if (Mathf.Abs(_rigidbody.velocity.x) > _settings.MaxSpeed)
            {
                _rigidbody.velocity = _rigidbody.velocity.normalized * _settings.MaxSpeed;
            }
        }

        private UnityArmatureComponent _armature;
        private Rigidbody2D _rigidbody;

        private PlayerCollisionData _collisionData;
        private PlayerInputData _inputData;
    }
}

