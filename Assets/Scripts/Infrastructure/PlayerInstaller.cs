using Assets.Scripts.Player.Components;
using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Settings;
using Assets.Scripts.Player.States;
using Assets.Scripts.Player.States.Concrete;
using UnityEngine;
using Zenject;

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
            InstallData();
            InstallStates();

            InstallInputComponent();
            InstallCollisionComponent();
            InstallStateComponent();

            Container.Bind<IPhysicComponent>().To<PhysicComponent>().AsSingle();
        }

        private void InstallStateComponent()
        {
            Container.Bind<IStateComponent>().To<StateComponent>().AsSingle();
        }

        private void InstallCollisionComponent()
        {
            Container.Bind<ICollisionComponent>().To<CollisionComponent>().AsSingle();
        }

        private void InstallInputComponent()
        {
            Container.Bind<MainPlayerInput>().AsSingle();

            Container.Bind<IInputComponent>().To<InputComponent>().AsSingle();
        }

        private void InstallPlayerPrefab()
        {
            Container.Bind<GameObject>().FromInstance(_playerPrefab).AsSingle();
        }

        private void InstallStates()
        {
            Container.Bind<StateBase>().WithId("PlayerIdleState").To<IdleState>().AsSingle();
            Container.Bind<StateBase>().WithId("PlayerWalkState").To<WalkState>().AsSingle();
            Container.Bind<StateBase>().WithId("PlayerJumpStartState").To<JumpStartState>().AsSingle();
            Container.Bind<StateBase>().WithId("PlayerFlyingState").To<FlyingState>().AsSingle();

            Container.Bind<StateMachine>().AsSingle();
        }

        private void InstallData()
        {
            Container.Bind<PlayerInputData>().AsSingle();
            Container.Bind<PlayerCollisionData>().AsSingle();
            Container.Bind<PlayerPhysicData>().AsSingle();
        }

        private void InstallSettings()
        {
            Container.Bind<PlayerSettings>().FromScriptableObject(_playerSettings).AsSingle();
        }
    }
}
