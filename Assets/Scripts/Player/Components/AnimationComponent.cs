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
            _eventBus.Subscribe(PlayerStates.Fall, PlayFallingAnimation);
            _eventBus.Subscribe(PlayerStates.Flip, PlayFlipAnimation);
            _eventBus.Subscribe(PlayerStates.MoveLeftBoost, PlayBoostAnimation);
            _eventBus.Subscribe(PlayerStates.MoveRightBoost, PlayBoostAnimation);
            _eventBus.Subscribe(PlayerStates.SimpleAttack, PlaySimpleAttackAnimation);
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
            _eventBus.Unsubscribe(PlayerStates.MoveLeftBoost, PlayBoostAnimation);
            _eventBus.Unsubscribe(PlayerStates.MoveRightBoost, PlayBoostAnimation);
            _eventBus.Unsubscribe(PlayerStates.SimpleAttack, PlaySimpleAttackAnimation);
        }

        private void PlayIdleAnimation()
        {
            _armature.animation.FadeIn(
                _settings.IdleAnimationName,
                _settings.IdleStateTransition);

            _armature.animation.timeScale = _settings.idleAnimationPlayingSpeed;
        }

        private void PlayMoveAnimation()
        {
            _armature.animation.FadeIn(
                _settings.WalkAnimationName,
                _settings.WalkStateTransition);

            _armature.animation.timeScale = _settings.walkAnimationPlayingSpeed;
        }

        private void PlayJumpStartAnimation()
        {
            _armature.animation.FadeIn(
                _settings.JumpStartAnimationName,
                _settings.JumpStartStateTransition,
                1);

            _armature.animation.timeScale = _settings.jumpStartAnimationPlayingSpeed;
        }
        
        private void PlayFallingAnimation()
        {
            _armature.animation.FadeIn(
                _settings.FallingAnimationName,
                _settings.FallingStateTransition);

            _armature.animation.timeScale = _settings.fallingAnimationPlayingSpeed;
        }

        private void PlayFlipAnimation()
        {
            _armature.armature.flipX = !_armature.armature.flipX;
        }

        private void PlayBoostAnimation()
        {
            _armature.animation.FadeIn(
                _settings.MoveBoostingAnimationName,
                _settings.MoveBoostingStateTransition);

            _armature.animation.timeScale = _settings.MoveBoostingAnimationPlayingSpeed;
        }
        private void PlaySimpleAttackAnimation()
        {
            _armature.animation.FadeIn(
                _settings.SimpleAttackAnimationName,
                _settings.SimpleAttackStateTransition,
                1);

            _armature.animation.timeScale = _settings.SimpleAttackAnimationPlayingSpeed;
        }

        private readonly UnityArmatureComponent _armature;
        private readonly PlayerSettings _settings;
    }
}