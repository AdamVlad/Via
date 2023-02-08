using Assets.Configs.InputSystem;
using Assets.Scripts.Player.Data;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Components
{
    public class InputComponent : IInputComponent
    {
        public InputComponent(
            MainPlayerInput input,
            ref PlayerInputData inputData)
        {
            _input = input;
            _inputData = inputData;
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
            _inputData.WalkButtonPressed = true;
            _inputData.AxisXPressedValue = callbackContext.ReadValue<Vector2>().x;
            _inputData.LeftWalkButtonPressed = _inputData.AxisXPressedValue < 0;
        }

        private void OnWalkingCanceled(InputAction.CallbackContext callbackContext)
        {
            _inputData.WalkButtonPressed = false;
        }

        private MainPlayerInput _input;
        private PlayerInputData _inputData;
    }
}
