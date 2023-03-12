using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

using Assets.Scripts.Extensions;

namespace Assets.Scripts.Effects.Lighting
{
    [RequireComponent(typeof(Light2D))]
    public class LightFlicker : MonoBehaviour
    {
        [SerializeField]
        private float _flickerIntensity = 0.5f;

        private void Awake()
        {
            _light = gameObject.GetComponent<Light2D>().IfNullThrowExceptionOrReturn();
            _baseIntensity = _light.intensity;
        }

        private void Update()
        {

            float noise = Mathf.PerlinNoise(Random.Range(0.0f, 1000.0f), Time.time);
            _light.intensity = Mathf.Lerp(_baseIntensity - _flickerIntensity, _baseIntensity, noise);
            
        }

        private float _baseIntensity;
        private Light2D _light;
    }
}