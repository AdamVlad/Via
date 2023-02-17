using System;
using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Settings;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.Components
{
    public class AnimationComponent : IAnimationComponent
    {
        public AnimationComponent(
            GameObject character,
            IEventBus<PLayerStates> eventBus,
            PlayerSettings settings)
        {
            _armature = character.GetComponentInChildren<UnityArmatureComponent>() ?? throw new NullReferenceException("AnimationComponent: UnityArmatureComponent is null"); ;
            _eventBus = eventBus ?? throw new NullReferenceException("AnimationComponent: IEventBus<PLayerStates> is null");
            _settings = settings ?? throw new NullReferenceException("AnimationComponent: PlayerSettings is null");
        }

        public void OnEnable()
        {
            _eventBus.Subscribe(PLayerStates.Idle, PlayIdleAnimation);
            _eventBus.Subscribe(PLayerStates.MoveLeft, PlayMoveAnimation);
            _eventBus.Subscribe(PLayerStates.MoveRight, PlayMoveAnimation);
            _eventBus.Subscribe(PLayerStates.JumpStart, PlayJumpStartAnimation);
            _eventBus.Subscribe(PLayerStates.Fly, PlayFlyingAnimation);
            _eventBus.Subscribe(PLayerStates.Flip, PlayFlipAnimation);
        }

        public void OnDisable()
        {
            _eventBus.Unsubscribe(PLayerStates.Idle, PlayIdleAnimation);
            _eventBus.Unsubscribe(PLayerStates.MoveLeft, PlayMoveAnimation);
            _eventBus.Unsubscribe(PLayerStates.MoveRight, PlayMoveAnimation);
            _eventBus.Unsubscribe(PLayerStates.JumpStart, PlayJumpStartAnimation);
            _eventBus.Unsubscribe(PLayerStates.Idle, PlayIdleAnimation);
            _eventBus.Unsubscribe(PLayerStates.Flip, PlayFlipAnimation);
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
        private readonly IEventBus<PLayerStates> _eventBus;
        private readonly PlayerSettings _settings;
    }
}