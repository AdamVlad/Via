using System;
using Zenject;

using Assets.Scripts.Player;
using Assets.Scripts.Utils.Factories;
using UnityEngine;

namespace Assets.Scripts.Infrastructure
{
    public sealed class FactoriesInstaller : MonoInstaller
    {
        [SerializeField] private PlayerSettings _playerSettings;

        private void Awake()
        {
            if (_playerSettings == null)
            {
                throw new NullReferenceException("Player settings not set");
            }
        }

        public override void InstallBindings()
        {
            InstallPlayerFactory();

            InstallFireBulletsFactory();
        }

        private void InstallPlayerFactory()
        {
            Container
                .BindFactory<Player.Player, PlayerFactory>()
                .FromComponentInNewPrefab(_playerSettings.PlayerPrefab);
        }

        private void InstallFireBulletsFactory()
        {
            Container
                .Bind<FireBulletsFactory>()
                .AsSingle();
        }
    }
}