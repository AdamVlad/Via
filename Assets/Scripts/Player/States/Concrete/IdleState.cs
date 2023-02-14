using Assets.Scripts.Player.Settings;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States.Concrete
{
    public sealed class IdleState : StateBase
    {
        public IdleState(
            GameObject character,
            PlayerSettings settings) : base(character, settings)
        {
            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            if (_armature == null) return;

            _armature.animation.FadeIn(_settings.IdleAnimationName, _settings.IdleStateTransition);
            _rigidbody.velocity = Vector3.zero;
        }

        private readonly UnityArmatureComponent _armature;
        private readonly Rigidbody2D _rigidbody;
    }
}