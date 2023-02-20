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

        public static T GetComponentInChildrenOrThrowException<T>(this GameObject gameObject) where T : Component
        {
            return gameObject.GetComponentInChildren<T>() ?? throw new NullReferenceException($"Game object {gameObject.name} does not have component {typeof(T)} in own child part");
        }

        public static GameObject IfNullThrowExceptionOrReturn(this GameObject gameObject)
        {
            return gameObject ?? throw new NullReferenceException("GameObject is null");
        }
    }
}
