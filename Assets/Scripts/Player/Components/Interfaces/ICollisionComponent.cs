using UnityEngine;

namespace Assets.Scripts.Player.Components.Interfaces
{
    public interface ICollisionComponent
    {
        void OnTriggerEnter2D(Collider2D collision);

        void OnTriggerExit2D(Collider2D collision);
    }
}