using UnityEngine;

namespace Assets.Script.Effects.Lighting
{
    public class LightFlicker : MonoBehaviour
    {
        public bool Flicker = true;

        public float FlickerIntensity = 0.5f;

        private float BaseIntensity;
        private Light LightComp;

        void Awake()
        {
            LightComp = gameObject.GetComponent<Light>();
            BaseIntensity = LightComp.intensity;
        }

        void Update()
        {
            if (Flicker)
            {
                float noise = Mathf.PerlinNoise(Random.Range(0.0f, 1000.0f), Time.time);
                LightComp.intensity = Mathf.Lerp(BaseIntensity - FlickerIntensity, BaseIntensity, noise);
            }
        }
    }
}