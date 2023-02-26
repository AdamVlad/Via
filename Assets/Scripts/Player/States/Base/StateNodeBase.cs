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

        public void SetChild(ref StateNodeBase child)
        {
            _childs.Add(child);
        }

        public bool CanEnter(ref DataComponent data)
        {
            return _condition(data);
        }

        public void EnterNextState(ref DataComponent data)
        {
            foreach (var state in _childs)
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
        protected IList<StateNodeBase> _childs = new List<StateNodeBase>();
        protected readonly Predicate<DataComponent> _condition;
    }
}