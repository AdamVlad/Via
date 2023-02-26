using System;
using System.Collections.Generic;

using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components;

namespace Assets.Scripts.Player.States.Base
{
    public abstract class StateNodeBase
    {
        protected StateNodeBase(
            ref StateMachine stateMachine,
            ref IEventBus<PlayerEvents> eventBus,
            Predicate<DataComponent> conditionForEnter)
        {
            _stateMachine = stateMachine;
            _eventBus = eventBus;
            _condition = conditionForEnter;
        }

        public abstract void Enter();

        public void SetLinks(params StateNodeBase[] linkedStates)
        {
            foreach (var state in linkedStates)
            {
                _links.Add(state);
            }
        }

        public bool CanEnter(ref DataComponent data)
        {
            return _condition(data);
        }

        public void EnterNextState(ref DataComponent data)
        {
            foreach (var state in _links)
            {
                if (state.CanEnter(ref data))
                {
                    _stateMachine.CurrentState = state;
                    state.Enter();
                    return;
                }
            }
        }

        protected StateMachine _stateMachine;
        protected IEventBus<PlayerEvents> _eventBus;
        protected IList<StateNodeBase> _links = new List<StateNodeBase>();
        protected readonly Predicate<DataComponent> _condition;
    }
}