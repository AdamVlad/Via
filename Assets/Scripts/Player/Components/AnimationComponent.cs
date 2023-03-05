using DragonBones;

using System.Threading.Tasks;

using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Utils.EventBus;
using AnimationState = DragonBones.AnimationState;

namespace Assets.Scripts.Player.Components
{
    public sealed class AnimationComponent : ObservableComponentDecorator
    {
        public AnimationComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
        }

        protected override void StartInternal()
        {
            base.StartInternal();

            _armature = _player.gameObject.GetComponentInChildrenOrThrowException<UnityArmatureComponent>();
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _eventBus.Subscribe(PlayerEvents.OnIdleStateEnter, PlayIdleAnimation);
            _eventBus.Subscribe(PlayerEvents.OnMoveLeftStateEnter, PlayMoveAnimation);
            _eventBus.Subscribe(PlayerEvents.OnMoveRightStateEnter, PlayMoveAnimation);
            _eventBus.Subscribe(PlayerEvents.OnJumpStartStateEnter, PlayJumpStartAnimation);
            _eventBus.Subscribe(PlayerEvents.OnFallStateEnter, PlayFallingAnimation);
            _eventBus.Subscribe(PlayerEvents.OnFlipPlayerPicture, PlayFlipAnimation);
            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, PlayBoostAnimation);
            _eventBus.Subscribe(PlayerEvents.OnBoostedMoveRightStateEnter, PlayBoostAnimation);
            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackStartStateEnter, PlaySimpleAttackStartAnimation);
            _eventBus.Subscribe(PlayerEvents.OnSimpleAttackEndStateEnter, PlaySimpleAttackEndAnimation);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _eventBus.Unsubscribe(PlayerEvents.OnIdleStateEnter, PlayIdleAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveLeftStateEnter, PlayMoveAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnMoveRightStateEnter, PlayMoveAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnJumpStartStateEnter, PlayJumpStartAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnIdleStateEnter, PlayIdleAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnFlipPlayerPicture, PlayFlipAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveLeftStateEnter, PlayBoostAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnBoostedMoveRightStateEnter, PlayBoostAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackStartStateEnter, PlaySimpleAttackStartAnimation);
            _eventBus.Unsubscribe(PlayerEvents.OnSimpleAttackEndStateEnter, PlaySimpleAttackEndAnimation);
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

        private void WaitForAnimationEnd(AnimationState animationState)
        {
            while (!animationState.isCompleted)
            {
            }

            Notify(new AttackData
            {
                IsSimpleAttackEnded = true
            });
        }

        private UnityArmatureComponent _armature;
    }
}