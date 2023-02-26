﻿using System;

using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components;
using Assets.Scripts.Player.States.Base;

namespace Assets.Scripts.Player.States
{
    public sealed class JumpStartWhenBoostedState : StateNodeBase
    {
        public JumpStartWhenBoostedState(
            ref StateMachine stateMachine,
            ref IEventBus<PlayerEvents> eventBus,
            Predicate<DataComponent> conditionForEnter) : base(ref stateMachine, ref eventBus, conditionForEnter)
        {
        }

        public override void Enter()
        {
            _eventBus.RaiseEvent(PlayerEvents.OnJumpStartWhenBoostedStateEnter);
        }
    }
}