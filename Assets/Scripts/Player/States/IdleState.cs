using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public sealed class IdleState : StateBase
    {
        public IdleState(GameObject character, StateMachine stateMachine) : base(character, stateMachine)
        {
            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
        }

        public override void Enter()
        {
            if (_armature == null) return;

            _armature.animation.FadeIn("Idle", 0.1f);
        }

        private UnityArmatureComponent _armature;
    }
}