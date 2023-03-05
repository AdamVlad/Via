using System;

using Assets.Scripts.Player.Components.Base;

namespace Assets.Scripts.Extensions
{
    public static class CharacterComponentsExtensions
    {
        public static T IfNullThrowExceptionOrReturn<T>(this T component) where T : ComponentBase
        {
            return component ?? throw new NullReferenceException($"Component {typeof(T)} is null");
        }
    }
}