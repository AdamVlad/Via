using Assets.Scripts.Patterns;
using Assets.Scripts.Player;
using Assets.Scripts.Player.Components;
using Assets.Scripts.Player.Components.Base;
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
            InstallEventBus();

            InstallInputComponent();
            InstallStateComponent();
            InstallAnimationComponent();
            InstallJumpComponent();
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
            Container.Bind<IEventBus<PlayerStates>>().To<EventBus<PlayerStates>>().AsSingle();
        }

        private void InstallInputComponent()
        {
            Container.Bind<MainPlayerInput>().AsSingle();
            Container.Bind<ComponentBase>().WithId("inputComponent").To<InputComponent>().AsSingle();
        }

        private void InstallStateComponent()
        {
            Container.Bind<ComponentBase>().WithId("stateComponent").To<StateComponent>().AsSingle();
        }

        private void InstallAnimationComponent()
        {
            Container.Bind<ComponentBase>().WithId("animationComponent").To<AnimationComponent>().AsSingle();
        }

        private void InstallJumpComponent()
        {
            Container.Bind<ComponentBase>().WithId("jumpComponent").To<JumpComponent>().AsSingle();
        }
    }
}
