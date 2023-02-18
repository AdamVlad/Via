﻿using System;
using UnityEngine;

namespace Assets.Scripts.Extensions
{
    public static class ComponentsExtensions
    {
        public static void IfNullThrowException<T>(this T component) where T : Component
        {
            if (component == null)
            {
                throw new NullReferenceException($"Component {typeof(T)} is null");
            }
        }

        public static T IfNullThrowExceptionOrReturn<T>(this T component) where T : Component
        {
            return component ?? throw new NullReferenceException($"Component {typeof(T)} is null");
        }
    }
}