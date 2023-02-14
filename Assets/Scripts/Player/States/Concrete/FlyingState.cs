using Assets.Scripts.Player.Settings;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States.Concrete
{
    public sealed class FlyingState : StateBase
    {
        public FlyingState(
            GameObject character,
            PlayerSettings settings) : base(character, settings)
        {
            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
        }

        public override void Enter()
        {
            _armature.animation.FadeIn(_settings.FlyingAnimationName, _settings.FlyingStateTransition);
        }

        private readonly UnityArmatureComponent _armature;
    }
}