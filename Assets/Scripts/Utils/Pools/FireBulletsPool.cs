using UnityEngine.Pool;
using UnityEngine;
using Zenject;

using Assets.Scripts.Entities.Bullets;
using Assets.Scripts.Utils.Factories;

namespace Assets.Scripts.Utils.Pools
{
    internal class FireBulletsPool : MonoBehaviour
    {
        [Inject]
        private void Construct(FireBulletsFactory factory)
        {
            _bulletFactory = factory;
        }

        [SerializeField] private int _poolSize;
        [SerializeField] private GameObject _bulletPrefab;

        public IObjectPool<FireBullet> Pool =>
            _pool ??= new ObjectPool<FireBullet>(
                CreatedPooledItem,
                OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject,
                true,
                _poolSize,
                _poolSize);

        private FireBullet CreatedPooledItem()
        {
            return _bulletFactory.Create(_bulletPrefab, _pool);
        }

        private void OnTakeFromPool(FireBullet bullet)
        {
            bullet.gameObject.SetActive(true);
        }

        private void OnReturnedToPool(FireBullet bullet)
        {
            bullet.gameObject.SetActive(false);
        }

        private void OnDestroyPoolObject(FireBullet bullet)
        {
            Destroy(bullet.gameObject);
        }

        private IObjectPool<FireBullet> _pool;
        private FireBulletsFactory _bulletFactory;
    }
}
