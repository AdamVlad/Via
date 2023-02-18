using Assets.Scripts.Patterns;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Player.Components
{
    public sealed class InputComponent : ObservableComponentDecorator
    {
        public InputComponent(
            MainPlayerInput input,
            IEventBus<PlayerStates> eventBus) : base(eventBus)
        {
            _input = input;
        }

        protected override void ActivateInternal()
        {
            _input.Actions.MoveLeft.started += OnMoveLeftButtonPressed;
            _input.Actions.MoveLeft.canceled += OnMoveLeftButtonCanceled;

            _input.Actions.MoveRight.started += OnMoveRightButtonPressed;
            _input.Actions.MoveRight.canceled += OnMoveRightButtonCanceled;

            _input.Actions.Jump.started += OnJumpButtonPressed;
            _input.Actions.Jump.canceled += OnJumpButtonCanceled;

            _input.Enable();
        }

        protected override void DeactivateInternal()
        {
            _input.Actions.MoveLeft.started -= OnMoveLeftButtonPressed;
            _input.Actions.MoveLeft.canceled -= OnMoveLeftButtonCanceled;

            _input.Actions.MoveRight.started -= OnMoveRightButtonPressed;
            _input.Actions.MoveRight.canceled -= OnMoveRightButtonCanceled;

            _input.Actions.Jump.started -= OnJumpButtonPressed;
            _input.Actions.Jump.canceled -= OnJumpButtonCanceled;

            _input.Disable();
        }

        private void OnMoveLeftButtonPressed(InputAction.CallbackContext callbackContext)
        {
            Notify(new InputData
            {
                MoveLeftButtonPressed = true,
                MoveRightButtonPressed = false,
                JumpButtonPressed = false
            });
        }

        private void OnMoveLeftButtonCanceled(InputAction.CallbackContext callbackContext)
        {
            Notify(new InputData
            {
                MoveLeftButtonPressed = false,
                MoveRightButtonPressed = false,
                JumpButtonPressed = false
            });
        }

        private void OnMoveRightButtonPressed(InputAction.CallbackContext callbackContext)
        {
            Notify(new InputData
            {
                MoveLeftButtonPressed = false,
                MoveRightButtonPressed = true,
                JumpButtonPressed = false
            });
        }

        private void OnMoveRightButtonCanceled(InputAction.CallbackContext callbackContext)
        {
            Notify(new InputData
            {
                MoveLeftButtonPressed = false,
                MoveRightButtonPressed = false,
                JumpButtonPressed = false
            });
        }

        private void OnJumpButtonPressed(InputAction.CallbackContext callbackContext)
        {
            Notify(new InputData
            {
                MoveLeftButtonPressed = false,
                MoveRightButtonPressed = false,
                JumpButtonPressed = true
            });
        }

        private void OnJumpButtonCanceled(InputAction.CallbackContext callbackContext)
        {
        }

        private readonly MainPlayerInput _input;
    }
}
