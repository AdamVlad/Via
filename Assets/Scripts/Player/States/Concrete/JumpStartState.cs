using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Settings;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States.Concrete
{
    public sealed class JumpStartState : StateBase
    {
        public JumpStartState(
            GameObject character,
            PlayerSettings settings, 
            PlayerCollisionData collisionData) : base(character, settings)
        {
            _collisionData = collisionData;

            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            _collisionData.OnGround = false;
            _armature?.animation.FadeIn(_settings.JumpStartAnimationName, _settings.JumpStartStateTransition, 1);
            _rigidbody?.AddForce(Vector2.up * _settings.JumpForce);
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