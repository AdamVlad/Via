using System;
using Zenject;
using UnityEngine;
using UnityEngine.Pool;

using Assets.Scripts.Utils.Pools;
using Assets.Scripts.Entities.Bullets;

namespace Assets.Scripts.Infrastructure
{
    public sealed class PoolsInstaller : MonoInstaller
    {
        [SerializeField]
        private FireBulletsPool _fireBulletsPool;

        private void Awake()
        {
            if (_fireBulletsPool == null)
            {
                throw new NullReferenceException("Fire bullets pool not set");
            }
        }

        public override void InstallBindings()
        {
            InstallFireBulletsPool();
        }

        private void InstallFireBulletsPool()
        {
            Container
                .Bind<IObjectPool<FireBullet>>()
                .FromInstance(_fireBulletsPool.Pool)
                .AsSingle();
        }
    }
}