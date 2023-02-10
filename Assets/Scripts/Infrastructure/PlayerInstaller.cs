using Assets.Scripts.Player.Settings;
using UnityEngine;
using Zenject;

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
            InstallSettings();
        }

        private void InstallSettings()
        {
            Container.Bind<PlayerSettings>().FromScriptableObject(_playerSettings).AsSingle();
        }
    }
}
