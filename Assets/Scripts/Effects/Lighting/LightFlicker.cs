using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

using Assets.Scripts.Extensions;

namespace Assets.Scripts.Effects.Lighting
{
    [RequireComponent(typeof(Light2D))]
    public class LightFlicker : MonoBehaviour
    {
        [SerializeField] private float _intensityChangedSpeedMultiplicator;

        [SerializeField] private float _intensityUpperBorder = 1;
        [SerializeField] private float _intensityBottomBorder = 0;

        private void Awake()
        {
            _light = gameObject.GetComponent<Light2D>().IfNullThrowExceptionOrReturn();
            _light.intensity = _intensityUpperBorder;

            var rand = new System.Random();
            _startDelay = (float)rand.NextDouble();
        }

        private void Update()
        {
            if (_startDelay > 0)
            {
                _startDelay -= Time.deltaTime;
                return;
            }

            if (_light.intensity >= _intensityUpperBorder)
            {
                _isGlowIncreaseDirection = false;
            }
            else if (_light.intensity <= _intensityBottomBorder)
            {
                _isGlowIncreaseDirection = true;
            }

            if (_isGlowIncreaseDirection)
            {
                _light.intensity += Time.deltaTime * _intensityChangedSpeedMultiplicator;
            }
            else
            {
                _light.intensity -= Time.deltaTime * _intensityChangedSpeedMultiplicator;
            }
        }

        private Light2D _light;
        private bool _isGlowIncreaseDirection;

        private float _startDelay;
    }
}