using UnityEngine;

namespace Assets.Scripts.Effects.StaffEffects.Base
{
    public abstract class StaffEffectsBase : MonoBehaviour
    {
        public virtual void Activate()
        {
            if (!gameObject.activeSelf)
            {
                gameObject.SetActive(true);
            }
        }

        public virtual void Deactivate()
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
            }
        }
    }
}