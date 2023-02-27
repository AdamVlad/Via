﻿using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.States.Base;

namespace Assets.Scripts.Player.States
{
    public sealed class BoostedMoveLeftState : StateNodeBase
    {
        public BoostedMoveLeftState(
            ref StateMachine stateMachine,
            ref IEventBus<PlayerEvents> eventBus) : base(ref stateMachine, ref eventBus)
        {
        }

        public override void Enter()
        {
            _eventBus.RaiseEvent(PlayerEvents.OnBoostedMoveLeftStateEnter);
        }
    }
}