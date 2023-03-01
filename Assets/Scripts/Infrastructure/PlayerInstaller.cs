using UnityEngine;
using Zenject;

using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Components;

namespace Assets.Scripts.Infrastructure
{
    public sealed class PlayerInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private PlayerSettings _playerSettings;

        private void Awake()
        {
            if (_playerPrefab == null)
            {
                Debug.LogError("PlayerInstaller: player prefab not set");
            }
            if (_playerSettings == null)
            {
                Debug.LogError("PlayerInstaller: player settings not set");
            }
        }

        public override void InstallBindings()
        {
            InstallPlayerPrefab();
            InstallSettings();
            InstallEventBus();

            InstallDataComponent();
            InstallInputComponent();
            InstallStateComponent();
            InstallAnimationComponent();
            InstallJumpComponent();
            InstallCollisionComponent();
            InstallFlipComponent();
            InstallMoveBoostingComponent();
            InstallMoveComponent();
            InstallFallTrackingComponent();
            InstallAttackComponent();
            InstallStaffEffectsComponent();
        }

        private void InstallPlayerPrefab()
        {
            Container.Bind<GameObject>().FromInstance(_playerPrefab).AsSingle();
        }

        private void InstallSettings()
        {
            Container.Bind<PlayerSettings>().FromScriptableObject(_playerSettings).AsSingle();
        }

        private void InstallEventBus()
        {
            Container.Bind<IEventBus<PlayerEvents>>().To<EventBus<PlayerEvents>>().AsSingle();
        }

        private void InstallDataComponent()
        {
            Container.Bind<DataComponent>().AsSingle();
        }

        private void InstallInputComponent()
        {
            Container.Bind<MainPlayerInput>().AsSingle();
            Container.Bind<InputComponent>().AsSingle();
        }

        private void InstallStateComponent()
        {
            Container.Bind<StateComponent>().AsSingle();
        }

        private void InstallAnimationComponent()
        {
            Container.Bind<AnimationComponent>().AsSingle();
        }

        private void InstallJumpComponent()
        {
            Container.Bind<JumpComponent>().AsSingle();
        }

        private void InstallCollisionComponent()
        {
            Container.Bind(typeof(GroundAndWallCheckerComponent), typeof(IFixedTickable)).To<GroundAndWallCheckerComponent>().AsSingle();
        }

        private void InstallFlipComponent()
        {
            Container.Bind<FlipComponent>().AsSingle();
        }

        private void InstallMoveBoostingComponent()
        {
            Container.Bind<MoveBoostComponent>().AsSingle();
        }

        private void InstallMoveComponent()
        {
            Container.Bind(typeof(MoveComponent), typeof(IFixedTickable)).To<MoveComponent>().AsSingle();
        }

        private void InstallFallTrackingComponent()
        {
            Container.Bind(typeof(FallTrackingComponent), typeof(IFixedTickable)).To<FallTrackingComponent>().AsSingle();
        }

        private void InstallAttackComponent()
        {
            Container.Bind<AttackComponent>().AsSingle();
        }

        private void InstallStaffEffectsComponent()
        {
            Container.Bind<StaffEffectsComponent>().AsSingle();
        }
    }
}
