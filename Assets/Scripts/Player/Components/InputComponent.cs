using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Components
{
    public class InputComponent : IInputComponent
    {
        public InputComponent(
            MainPlayerInput input,
            InputData inputData)
        {
            _input = input;
            _inputData = inputData;
        }

        public void Start()
        {
            _input.Actions.MoveLeft.started += OnMoveLeftButtonPressed;
            _input.Actions.MoveLeft.canceled += OnMoveLeftButtonCanceled;

            _input.Actions.MoveRight.started += OnMoveRightButtonPressed;
            _input.Actions.MoveRight.canceled += OnMoveRightButtonCanceled;

            _input.Actions.Jump.started += OnJumpButtonPressed;
            _input.Actions.Jump.canceled += OnJumpButtonCanceled;
        }

        public void Destroy()
        {
            _input.Actions.MoveLeft.started -= OnMoveLeftButtonPressed;
            _input.Actions.MoveLeft.canceled -= OnMoveLeftButtonCanceled;

            _input.Actions.MoveRight.started -= OnMoveRightButtonPressed;
            _input.Actions.MoveRight.canceled -= OnMoveRightButtonCanceled;

            _input.Actions.Jump.started -= OnJumpButtonPressed;
            _input.Actions.Jump.canceled -= OnJumpButtonCanceled;
        }

        private void OnMoveLeftButtonPressed(InputAction.CallbackContext callbackContext)
        {
            _inputData.MoveLeftButtonPressed = true;
        }

        private void OnMoveLeftButtonCanceled(InputAction.CallbackContext callbackContext)
        {
            _inputData.MoveLeftButtonPressed = false;
        }

        private void OnMoveRightButtonPressed(InputAction.CallbackContext callbackContext)
        {
            _inputData.MoveRightButtonPressed = true;
        }

        private void OnMoveRightButtonCanceled(InputAction.CallbackContext callbackContext)
        {
            _inputData.MoveRightButtonPressed = false;
        }

        private void OnJumpButtonPressed(InputAction.CallbackContext callbackContext)
        {
            _inputData.JumpButtonPressed = true;
        }

        private void OnJumpButtonCanceled(InputAction.CallbackContext callbackContext)
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

        private readonly MainPlayerInput _input;
        private readonly InputData _inputData;
    }
}
