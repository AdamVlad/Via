﻿using Assets.Scripts.Player.States.Base;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.States
{
    public sealed class BoostedMoveRightState : StateNodeBase
    {
        public BoostedMoveRightState(
            ref StateMachine stateMachine,
            ref IEventBus<PlayerEvents> eventBus) : base(ref stateMachine, ref eventBus)
        {
        }

        public override void Enter()
        {
            _eventBus.RaiseEvent(PlayerEvents.OnBoostedMoveRightStateEnter);
        }
    }
}