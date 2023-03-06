using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Zenject;

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

        public void GiveMovementTo(Vector3 endPoint)
        {
            var convertedToVector2StartPoint = new Vector2(transform.position.x, transform.position.y);
            var convertedToVector2EndPoint = new Vector2(endPoint.x, endPoint.y);
            _direction = (convertedToVector2EndPoint - convertedToVector2StartPoint).normalized;

            _dirty = true;

            StartCoroutine(RunLifeCycle());
        }

        private void FixedUpdate()
        {
            if (_dirty)
            {
                transform.Translate(
                    _direction *
                    _speed *
                    Time.fixedDeltaTime);
            }
        }

        private void OnDisable()
        {
            _dirty = false;
            StopCoroutine(RunLifeCycle());
        }

        private IEnumerator RunLifeCycle()
        {
            yield return new WaitForSeconds(_lifeTime);

            _dirty = false;
            _pool.Release(this);
        }

        private IObjectPool<FireBullet> _pool;

        private Vector3 _direction;

        private bool _dirty;
    }
}