using System;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class GameObjectExtensions
    {
        public static T GetComponentOrThrowException<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponent<T>() ?? throw new NullReferenceException($"Game object {gameObject.name} does not have component {typeof(T)}");
        }
    }
}
