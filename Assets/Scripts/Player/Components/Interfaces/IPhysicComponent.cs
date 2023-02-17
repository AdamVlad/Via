﻿namespace Assets.Scripts.Player.Components.Interfaces
{
    public interface IPhysicComponent
    {
        void OnEnable();
        void OnDisable();
        void FixedUpdate();
    }
}