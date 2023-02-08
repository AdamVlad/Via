using Assets.Scripts.Player.Data;
using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public sealed class WalkState : StateBase
    {
        public WalkState(GameObject character, StateMachine stateMachine, ref PlayerInputData inputData) : base(character, stateMachine)
        {
            _inputData = inputData;

            _armature = character.GetComponentInChildren<UnityArmatureComponent>();
            _rigidbody = character.GetComponent<Rigidbody2D>();
        }

        public override void Enter()
        {
            if (_armature == null) return;

            _armature.armature.flipX = _inputData.LeftWalkButtonPressed;
            _armature.animation.FadeIn("Walk", 0.1f);
        }

        public override void FixedUpdate()
        {
            _rigidbody?.AddForce(Vector2.right * _inputData.AxisXPressedValue * 30);
        }

        private UnityArmatureComponent _armature;
        private Rigidbody2D _rigidbody;

        private PlayerInputData _inputData;
    }
}

