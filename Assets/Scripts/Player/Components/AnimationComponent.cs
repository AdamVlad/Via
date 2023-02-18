using System;
using Assets.Scripts.Patterns;
using Assets.Scripts.Player.Components.Base;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public sealed class AnimationComponent : ComponentBase
    {
        public AnimationComponent(
            IEventBus<PlayerStates> eventBus,
            GameObject character,
            PlayerSettings settings) : base(eventBus)
        {
            _armature = character.GetComponentInChildren<UnityArmatureComponent>() ?? throw new NullReferenceException("AnimationComponent: UnityArmatureComponent is null");
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            _eventBus.Subscribe(PlayerStates.Idle, PlayIdleAnimation);
            _eventBus.Subscribe(PlayerStates.MoveLeft, PlayMoveAnimation);
            _eventBus.Subscribe(PlayerStates.MoveRight, PlayMoveAnimation);
            _eventBus.Subscribe(PlayerStates.JumpStart, PlayJumpStartAnimation);
            _eventBus.Subscribe(PlayerStates.Fly, PlayFlyingAnimation);
            _eventBus.Subscribe(PlayerStates.Flip, PlayFlipAnimation);
        }

        protected override void DeactivateInternal()
        {
            _eventBus.Unsubscribe(PlayerStates.Idle, PlayIdleAnimation);
            _eventBus.Unsubscribe(PlayerStates.MoveLeft, PlayMoveAnimation);
            _eventBus.Unsubscribe(PlayerStates.MoveRight, PlayMoveAnimation);
            _eventBus.Unsubscribe(PlayerStates.JumpStart, PlayJumpStartAnimation);
            _eventBus.Unsubscribe(PlayerStates.Idle, PlayIdleAnimation);
            _eventBus.Unsubscribe(PlayerStates.Flip, PlayFlipAnimation);
        }

        private void PlayIdleAnimation()
        {
            _armature.animation.FadeIn(_settings.IdleAnimationName, _settings.IdleStateTransition);
        }

        private void PlayMoveAnimation()
        {
            _armature.animation.FadeIn(_settings.WalkAnimationName, _settings.WalkStateTransition);
        }

        private void PlayJumpStartAnimation()
        {
            _armature.animation.FadeIn(_settings.JumpStartAnimationName, _settings.JumpStartStateTransition, 1);
        }
        
        private void PlayFlyingAnimation()
        {
            _armature.animation.FadeIn(_settings.FlyingAnimationName, _settings.FlyingStateTransition);
        }

        private void PlayFlipAnimation()
        {
            _armature.armature.flipX = !_armature.armature.flipX;
        }

        private readonly UnityArmatureComponent _armature;
        private readonly PlayerSettings _settings;
    }
}