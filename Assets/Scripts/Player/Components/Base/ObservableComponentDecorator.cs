﻿using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Patterns.Observer;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Player.ComponentsData.Interfaces;

namespace Assets.Scripts.Player.Components.Base
{
    public abstract class ObservableComponentDecorator : ComponentBase
    {
        protected ObservableComponentDecorator(IEventBus<PlayerEvents> eventBus) : base(eventBus)
        {
            _observable = new Observable();
        }

        public void AddObserver(IObserver observer)
        {
            _observable.AddObserver(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observable.RemoveObserver(observer);
        }

        public void Notify(IData data)
        {
            _observable.Notify(ref data);
        }

        private readonly Observable _observable;
    }
}