using Assets.Scripts.Player;
using Assets.Scripts.Player.Components;
using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Settings;
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
            InstallEventBus();

            InstallInputComponent();
            InstallCollisionComponent();
            InstallStateComponent();
            InstallAnimationComponent();
            InstallPhysicComponent();
        }

        private void InstallPlayerPrefab()
        {
            Container.Bind<GameObject>().FromInstance(_playerPrefab).AsSingle();
        }

        private void InstallSettings()
        {
            Container.Bind<PlayerSettings>().FromScriptableObject(_playerSettings).AsSingle();
        }

        private void InstallData()
        {
            Container.Bind<InputData>().AsSingle();
            Container.Bind<CollisionData>().AsSingle();
            Container.Bind<PhysicData>().AsSingle();
        }

        private void InstallEventBus()
        {
            Container.Bind<IEventBus<PLayerStates>>().To<EventBus<PLayerStates>>().AsSingle();
        }

        private void InstallInputComponent()
        {
            Container.Bind<MainPlayerInput>().AsSingle();
            Container.Bind<IInputComponent>().To<InputComponent>().AsSingle();
        }

        private void InstallCollisionComponent()
        {
            Container.Bind<ICollisionComponent>().To<CollisionComponent>().AsSingle();
        }

        private void InstallStateComponent()
        {
            Container.Bind<IStateComponent>().To<StateComponent>().AsSingle();
        }

        private void InstallAnimationComponent()
        {
            Container.Bind<IAnimationComponent>().To<AnimationComponent>().AsSingle();
        }

        private void InstallPhysicComponent()
        {
            Container.Bind<IPhysicComponent>().To<PhysicComponent>().AsSingle();
        }
    }
}
