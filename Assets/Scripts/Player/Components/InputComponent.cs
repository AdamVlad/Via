using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;

using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Components
{
    public class InputComponent : IInputComponent
    {
        public InputComponent(
            MainPlayerInput input,
            PlayerInputData inputData)
        {
            _input = input;
            _inputData = inputData;
        }

        public void Start()
        {
            _input.Actions.Walk.started += OnWalkButtonPressed;
            _input.Actions.Walk.canceled += OnWalkButtonReleased;

            _input.Actions.Jump.started += OnJumpButtonPressed;
            _input.Actions.Jump.canceled += OnJumpButtonReleased;
        }

        public void Destroy()
        {
            _input.Actions.Walk.started -= OnWalkButtonPressed;
            _input.Actions.Walk.canceled -= OnWalkButtonReleased;

            _input.Actions.Jump.started -= OnJumpButtonPressed;
            _input.Actions.Jump.started -= OnJumpButtonReleased;
        }

        private void OnWalkButtonPressed(InputAction.CallbackContext callbackContext)
        {
            _inputData.WalkButtonPressed = true;
            _inputData.AxisXPressedValue = callbackContext.ReadValue<Vector2>().x;
            _inputData.LeftWalkButtonPressed = _inputData.AxisXPressedValue < 0;
        }

        private void OnWalkButtonReleased(InputAction.CallbackContext callbackContext)
        {
            _inputData.WalkButtonPressed = false;
        }

        private void OnJumpButtonPressed(InputAction.CallbackContext callbackContext)
        {
            Debug.Log("Jump button pressed");
            _inputData.JumpButtonPressed = true;
        }

        private void OnJumpButtonReleased(InputAction.CallbackContext callbackContext)
        {
            _inputData.JumpButtonPressed = false;
        }
        
        public void InputOn()
        {
            _input.Enable();
        }

        public void InputOff()
        {
            _input.Disable();
        }

        private MainPlayerInput _input;
        private PlayerInputData _inputData;
    }
}
