using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public sealed class FlyingState : StateBase
    {
        public FlyingState(GameObject character, StateMachine stateMachine) : base(character, stateMachine)
        {
            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
        }

        public override void Enter()
        {
            _armature?.animation.FadeIn("Flying");
        }

        private UnityArmatureComponent _armature;
    }
}