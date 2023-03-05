using System;
using Zenject;

using Assets.Scripts.Player;

namespace Assets.Scripts.Infrastructure
{
    public sealed class FactoriesInstaller : MonoInstaller
    {
        [Inject]
        PlayerSettings _settings;

        private void Awake()
        {
            if (_settings == null)
            {
                throw new NullReferenceException("Player settings not set");
            }
        }

        public override void InstallBindings()
        {
            InstallPlayerFactory();
        }

        private void InstallPlayerFactory()
        {
            Container
                .BindFactory<Player.Player, Factory>()
                .FromComponentInNewPrefab(_settings.PlayerPrefab);
        }
    }
}