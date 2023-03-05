using Assets.Scripts.Player.ComponentsData.Interfaces;
using Assets.Scripts.Utils.EventBus;
using Assets.Scripts.Utils.Observer;

namespace Assets.Scripts.Player.Components.Base
{
    public abstract class ObservableComponentDecorator : ComponentBase
    {
        protected ObservableComponentDecorator(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
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