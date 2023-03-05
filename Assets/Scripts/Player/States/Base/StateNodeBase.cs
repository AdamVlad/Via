using System;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.Player.Components;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.States.Base
{
    public abstract class StateNodeBase
    {
        protected StateNodeBase(
            ref StateMachine stateMachine,
            ref IEventBus<PlayerEvents> eventBus)
        {
            _stateMachine = stateMachine;
            _eventBus = eventBus;
        }

        public abstract void Enter();

        public void SetLink(ref StateNodeBase linkedState, Predicate<DataComponent> conditionForEnter)
        {
            try
            {
                _links.Add(linkedState, conditionForEnter);
            }
            catch (ArgumentException ex)
            {
                Debug.LogError($"StateNodeBase: SetLink exception {ex}");
            }
        }

        public void EnterNextState(ref DataComponent data)
        {
            foreach (var state in _links)
            {
                if (state.Value(data))
                {
                    _stateMachine.CurrentState = state.Key;
                    _stateMachine.CurrentState.Enter();
                    return;
                }
            }
        }

        protected StateMachine _stateMachine;
        protected IEventBus<PlayerEvents> _eventBus;
        protected IDictionary<StateNodeBase, Predicate<DataComponent>> _links = new Dictionary<StateNodeBase, Predicate<DataComponent>>();
    }
}