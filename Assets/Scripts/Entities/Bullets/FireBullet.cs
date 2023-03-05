using System;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

using System.Collections;

namespace Assets.Scripts.Entities.Bullets
{
    public class FireBullet : MonoBehaviour
    {
        [Inject]
        private void Construct(IObjectPool<FireBullet> pool)
        {
            _pool = pool ?? throw new NullReferenceException("FireBullet: IObjectPool<FireBullet> not set");
        }

        [SerializeField, Range(0, 5)] private float _lifeTime = 4;
        [SerializeField] private float _speed;

        private void FixedUpdate()
        {
            if (_dirty)
            {
                transform.Translate(
                    Vector3.right *
                    _speed *
                    Time.fixedDeltaTime);
            }
        }

        private void OnEnable()
        {
            _dirty = true;
            StartCoroutine(RunLifeCycle());
        }

        private void OnDisable()
        {
            _dirty = false;
        }

        private void OnDestroy()
        {
            StopCoroutine(RunLifeCycle());
        }

        private IEnumerator RunLifeCycle()
        {
            yield return new WaitForSeconds(_lifeTime);

            _dirty = false;
            _pool.Release(this);
        }

        private IObjectPool<FireBullet> _pool;

        private bool _dirty;
    }
}
