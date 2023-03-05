using UnityEngine;
using UnityEngine.Pool;
using Zenject;

using Assets.Scripts.Entities.Bullets;

namespace Assets.Scripts.Utils.Factories
{
    public class FireBulletsFactory : IFactory<GameObject, IObjectPool<FireBullet>, FireBullet>
    {
        public FireBullet Create(GameObject prefab, IObjectPool<FireBullet> pool)
        {
            var bulletGameObject = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity);
            return bulletGameObject.GetComponent<FireBullet>();
        }
    }
}