using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Settings;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States.Concrete
{
    public sealed class IdleState : StateBase
    {
        public IdleState(
            GameObject character,
            StateMachine stateMachine,
            PlayerSettings settings, 
            ref PlayerCollisionData collisionData) : base(character, stateMachine, settings)
        {
            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
            _rigidbody = character.GetComponent<Rigidbody2D>();

            _collisionData = collisionData;
        }

        public override void Enter()
        {
            if (_armature == null) return;

            _armature.animation.FadeIn(_settings.IdleAnimationName, _settings.IdleStateTransition);
            _rigidbody.velocity = Vector3.zero;

            _collisionData.OnGround = true;
            _collisionData.Flying = false;
        }

        private PlayerCollisionData _collisionData;

        private UnityArmatureComponent _armature;
        private Rigidbody2D _rigidbody;
    }
}