using System;
using DragonBones;
using UnityEngine;

using Assets.Scripts.Extensions;
using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;

namespace Assets.Scripts.Player.Components
{
    public sealed class AnimationComponent : ComponentBase
    {
        public AnimationComponent(
            IEventBus<PlayerStates> eventBus,
            GameObject character,
            PlayerSettings settings) : base(eventBus)
        {
            _armature = character.GetComponentInChildrenOrThrowException<UnityArmatureComponent>();
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerStates.Idle, PlayIdleAnimation);
            _eventBus.Subscribe(PlayerStates.MoveLeft, PlayMoveAnimation);
            _eventBus.Subscribe(PlayerStates.MoveRight, PlayMoveAnimation);
            _eventBus.Subscribe(PlayerStates.JumpStart, PlayJumpStartAnimation);
            _eventBus.Subscribe(PlayerStates.Fly, PlayFlyingAnimation);
            _eventBus.Subscribe(PlayerStates.Flip, PlayFlipAnimation);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

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
            _armature.animation.timeScale = _settings.IdleStatePlayingSpeed;
        }

        private void PlayMoveAnimation()
        {
            _armature.animation.FadeIn(_settings.WalkAnimationName, _settings.WalkStateTransition);
            _armature.animation.timeScale = _settings.WalkStatePlayingSpeed;
        }

        private void PlayJumpStartAnimation()
        {
            _armature.animation.FadeIn(_settings.JumpStartAnimationName, _settings.JumpStartStateTransition, 1);
            _armature.animation.timeScale = _settings.JumpStartStatePlayingSpeed;
        }
        
        private void PlayFlyingAnimation()
        {
            _armature.animation.FadeIn(_settings.FlyingAnimationName, _settings.FlyingStateTransition);
            _armature.animation.timeScale = _settings.FlyingStatePlayingSpeed;
        }

        private void PlayFlipAnimation()
        {
            _armature.armature.flipX = !_armature.armature.flipX;
        }

        private readonly UnityArmatureComponent _armature;
        private readonly PlayerSettings _settings;
    }
}