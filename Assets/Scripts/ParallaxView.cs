using UnityEngine;

namespace Assets.Scripts
{
    public class ParallaxView : MonoBehaviour
    {
        [SerializeField] private float _shiftFactor;

        private void Start()
        {
            _followingTarget = Camera.main.transform;
        }

        private void FixedUpdate()
        {
            var delta = _followingTarget.position - _previousTargetPosition;
            _previousTargetPosition = _followingTarget.position;

            delta.y = 0;

            transform.position += delta * _shiftFactor;
        }

        private Transform _followingTarget;
        private Vector3 _previousTargetPosition;
    }
}