using System.Collections.Generic;
using UnityEngine.Events;

namespace Assets.Scripts.Utils.EventBus
{
    public class EventBus<TEvent> : IEventBus<TEvent>
    {
        public void Subscribe(TEvent eventType, UnityAction action)
        {
            if (_events.TryGetValue(eventType, out var thisEvent))
            {
                thisEvent.AddListener(action);
            }
            else
            {
                thisEvent = new UnityEvent();
                thisEvent.AddListener(action);
                _events.Add(eventType, thisEvent);
            }
        }

        public void Unsubscribe(TEvent playerState, UnityAction action)
        {
            if (_events.TryGetValue(playerState, out var thisEvent))
            {
                thisEvent.RemoveListener(action);
            }
        }

        public void RaiseEvent(TEvent playerState)
        {
            if(_events.TryGetValue(playerState, out var thisEvent))
            {
                thisEvent.Invoke();
            }
        }

        private readonly IDictionary<TEvent, UnityEvent> _events = new Dictionary<TEvent, UnityEvent>();
    }
}