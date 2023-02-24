using System.Collections.Generic;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Player.ComponentsData.Interfaces;

namespace Assets.Scripts.Patterns.Observer
{
    public abstract class ObservableBase : IObservable
    {
        protected ObservableBase()
        {
            _observers = new List<IObserver>();
        }

        public void AddObserver(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void RemoveObserver(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public void Notify(ref IData data)
        {
            foreach (var observer in _observers)
            {
                observer.Update(ref data);
            }
        }

        private readonly List<IObserver> _observers;
    }
}