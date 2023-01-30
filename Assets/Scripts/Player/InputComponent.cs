using DragonBones;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player
{
    public class InputComponent : IInputComponent
    {
        public InputComponent(
            MainPlayerInput input,
            UnityArmatureComponent armature,
            Rigidbody2D rigidbody)
        {
            _input = input;
            _armature = armature;
            _rigidbody = rigidbody;
        }

        public void Start()
        {
            _input.Actions.Walk.started += OnWalkingStarted;
            _input.Actions.Walk.canceled += OnWalkingCanceled;
        }

        public void Destroy()
        {
            _input.Actions.Walk.started -= OnWalkingStarted;
            _input.Actions.Walk.canceled -= OnWalkingCanceled;
        }

        public void InputOn()
        {
            _input.Enable();
        }

        public void InputOff()
        {
            _input.Disable();
        }

        private void OnWalkingStarted(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("OnWalkingStarted");

            _direction = callbackContext.ReadValue<Vector2>().x;

            _armature.armature.flipX = !(_direction > 0);

            _armature.animation.FadeIn("Walk", 0.1f);

            _rigidbody.AddForce(Vector2.right * _direction * 100);

        }

        private void OnWalkingCanceled(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("OnWalkingCanceled");

            _direction = callbackContext.ReadValue<Vector2>().x;

            _armature.animation.FadeIn("Idle", 0.1f);

        }

        private MainPlayerInput _input;
        private UnityArmatureComponent _armature;
        private Rigidbody2D _rigidbody;

        private float _direction;
    }
}

