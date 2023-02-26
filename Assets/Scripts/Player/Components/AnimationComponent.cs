using System;
using DragonBones;
using UnityEngine;

using Assets.Scripts.Extensions;
using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;
using System.Threading.Tasks;
using Assets.Scripts.Player.ComponentsData;

namespace Assets.Scripts.Player.Components
{
    public sealed class AnimationComponent : ObservableComponentDecorator
    {
        public AnimationComponent(
            IEventBus<PlayerEvents> eventBus,
            GameObject character,
            PlayerSettings settings) : base(eventBus)
        {
            _armature = character.GetComponentInChildrenOrThrowException<UnityArmatureComponent>();
            _settings = settings ?? throw new NullReferenceException("PlayerSettings is null");
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnIdleStateEnter, PlayIdleAnimation);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftStateEnter, PlayMoveAnimation);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightStateEnter, PlayMoveAnimation);
            _eventBus.Subscribe(PlayerEvents.OnJumpStartStateEnter, PlayJumpStartAnimation);
            _eventBus.Subscribe(PlayerEvents.OnFallStateEnter, PlayFallingAnimation);
            _eventBus.Subscribe(PlayerEvents.Flip, PlayFlipAnimation);
            _eventBus.Subscribe(PlayerEvents.MoveLeftBoost, PlayBoostAnimation);
            _eventBus.Subscribe(PlayerEvents.MoveRightBoost, PlayBoostAnimation);
            _eventBus.Subscribe(PlayerEvents.SimpleAttackStart, PlaySimpleAttackStartAnimation);
            _eventBus.Subscribe(PlayerEvents.SimpleAttackEnd, PlaySimpleAttackEndAnimation);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnIdleStateEnter, PlayIdleAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftStateEnter, PlayMoveAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightStateEnter, PlayMoveAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnJumpStartStateEnter, PlayJumpStartAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnIdleStateEnter, PlayIdleAnimation);
            _eventBus.Unsubscribe(PlayerEvents.Flip, PlayFlipAnimation);
            _eventBus.Unsubscribe(PlayerEvents.MoveLeftBoost, PlayBoostAnimation);
            _eventBus.Unsubscribe(PlayerEvents.MoveRightBoost, PlayBoostAnimation);
            _eventBus.Unsubscribe(PlayerEvents.SimpleAttackStart, PlaySimpleAttackStartAnimation);
            _eventBus.Unsubscribe(PlayerEvents.SimpleAttackEnd, PlaySimpleAttackEndAnimation);
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
        private void PlaySimpleAttackStartAnimation()
        {
            _armature.animation.FadeIn(
                _settings.simpleAttackStartAnimationName,
                _settings.simpleAttackStartStateTransition,
                1);

            _armature.animation.timeScale = _settings.simpleAttackStartAnimationPlayingSpeed;
        }

        private void PlaySimpleAttackEndAnimation()
        {
            var animationState = _armature.animation.FadeIn(
                _settings.simpleAttackEndAnimationName,
                _settings.simpleAttackEndStateTransition,
                1);

            _armature.animation.timeScale = _settings.simpleAttackEndAnimationPlayingSpeed;

            Task.Factory.StartNew(() => WaitForAnimationEnd(animationState));
        }

        private void WaitForAnimationEnd(DragonBones.AnimationState animationState)
        {
            while (!animationState.isCompleted)
            {
            }

            Debug.Log("1");
            Notify(new NullData());
        }

        private readonly UnityArmatureComponent _armature;
        private readonly PlayerSettings _settings;
    }
}