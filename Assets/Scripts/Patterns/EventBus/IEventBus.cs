using UnityEngine.Events;

namespace Assets.Scripts.Patterns
{
    public interface IEventBus<in TEvent>
    {
        void Subscribe(TEvent eventType, UnityAction action);

        void Unsubscribe(TEvent playerState, UnityAction action);

        void RaiseEvent(TEvent playerState);
    }
}