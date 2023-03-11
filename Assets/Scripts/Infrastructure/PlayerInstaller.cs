using UnityEngine;
using Zenject;

using Assets.Scripts.Player;
using Assets.Scripts.Player.Components;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Infrastructure
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSettings _playerSettings;

        private void Awake()
        {
            if (_playerSettings == null)
            {
                Debug.LogError("PlayerInstaller: player settings not set");
            }
        }

        public override void InstallBindings()
        {
            InstallEventBus();
            InstallSettings();
            
            InstallDataComponent();
            InstallInputComponent();
            InstallStateComponent();
            InstallAnimationComponent();
            InstallJumpComponent();
            InstallGroundAndWallCheckerComponent();
            InstallFlipComponent();
            InstallMoveBoostingComponent();
            InstallMoveComponent();
            InstallFallTrackingComponent();
            InstallCursorCaptureComponent();
            InstallAttackComponent();
            InstallStaffEffectsComponent();
            InstallCameraCaptureComponent();
        }

        private void InstallSettings()
        {
            Container
                .Bind<PlayerSettings>()
                .FromScriptableObject(_playerSettings)
                .AsSingle();
        }

        private void InstallEventBus()
        {
            Container
                .Bind<IEventBus<PlayerEvents>>()
                .To<EventBus<PlayerEvents>>()
                .AsSingle();
        }

        private void InstallDataComponent()
        {
            Container
                .Bind<DataComponent>()
                .AsSingle();
        }

        private void InstallInputComponent()
        {
            Container
                .Bind<InputComponent>()
                .AsSingle();
        }

        private void InstallStateComponent()
        {
            Container
                .Bind<StateComponent>()
                .AsSingle();
        }

        private void InstallAnimationComponent()
        {
            Container
                .Bind<AnimationComponent>()
                .AsSingle();
        }

        private void InstallJumpComponent()
        {
            Container
                .Bind<JumpComponent>()
                .AsSingle();
        }

        private void InstallGroundAndWallCheckerComponent()
        {
            Container
                .Bind(typeof(GroundAndWallCheckerComponent), typeof(IFixedTickable))
                .To<GroundAndWallCheckerComponent>()
                .AsSingle();
        }

        private void InstallFlipComponent()
        {
            Container
                .Bind(typeof(FlipComponent), typeof(ITickable))
                .To<FlipComponent>()
                .AsSingle();
        }

        private void InstallMoveBoostingComponent()
        {
            Container
                .Bind<MoveBoostComponent>()
                .AsSingle();
        }

        private void InstallMoveComponent()
        {
            Container
                .Bind(typeof(MoveComponent), typeof(IFixedTickable))
                .To<MoveComponent>()
                .AsSingle();
        }

        private void InstallFallTrackingComponent()
        {
            Container
                .Bind(typeof(FallTrackingComponent), typeof(IFixedTickable))
                .To<FallTrackingComponent>()
                .AsSingle();
        }

        private void InstallCursorCaptureComponent()
        {
            Container
                .Bind<CursorCaptureComponent>()
                .AsSingle();
        }

        private void InstallAttackComponent()
        {
            Container
                .Bind<AttackComponent>()
                .AsSingle();
        }

        private void InstallStaffEffectsComponent()
        {
            Container
                .Bind<StaffEffectsComponent>()
                .AsSingle();
        }

        private void InstallCameraCaptureComponent()
        {
            Container
                .Bind(typeof(CameraCaptureComponent), typeof(ITickable))
                .To<CameraCaptureComponent>()
                .AsSingle();
        }
    }
}