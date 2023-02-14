using Assets.Scripts.Player.Settings;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States.Concrete
{
    public sealed class JumpStartState : StateBase
    {
        public JumpStartState(
            GameObject character,
            PlayerSettings settings) : base(character, settings)
        {
            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            _armature?.animation.FadeIn(_settings.JumpStartAnimationName, _settings.JumpStartStateTransition, 1);
            _rigidbody?.AddForce(Vector2.up * _rigidbody.mass * _settings.JumpForce);
        }

        private readonly UnityArmatureComponent _armature;
        private readonly Rigidbody2D _rigidbody;
    }
}