using UnityEngine.InputSystem;

using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components
{
    public sealed class InputComponent : ObservableComponentDecorator
    {
        public InputComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
            _input = new MainPlayerInput();
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _input.Actions.MoveLeft.started += OnMoveLeftButtonPressed;
            _input.Actions.MoveLeft.canceled += OnMoveLeftButtonCanceled;

            _input.Actions.MoveRight.started += OnMoveRightButtonPressed;
            _input.Actions.MoveRight.canceled += OnMoveRightButtonCanceled;

            _input.Actions.MoveBoost.started += OnMoveBoostButtonPressed;
            _input.Actions.MoveBoost.canceled += OnMoveBoostButtonCanceled;

            _input.Actions.Jump.started += OnJumpButtonPressed;
            _input.Actions.Jump.canceled += OnJumpButtonCanceled;

            _input.Actions.SimpleAttack.started += OnSimpleAttackPressed;
            _input.Actions.SimpleAttack.canceled += OnSimpleAttackCanceled;

            _input.Enable();
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _input.Actions.MoveLeft.started -= OnMoveLeftButtonPressed;
            _input.Actions.MoveLeft.canceled -= OnMoveLeftButtonCanceled;

            _input.Actions.MoveRight.started -= OnMoveRightButtonPressed;
            _input.Actions.MoveRight.canceled -= OnMoveRightButtonCanceled;

            _input.Actions.MoveBoost.started -= OnMoveBoostButtonPressed;
            _input.Actions.MoveBoost.canceled -= OnMoveBoostButtonCanceled;

            _input.Actions.Jump.started -= OnJumpButtonPressed;
            _input.Actions.Jump.canceled -= OnJumpButtonCanceled;

            _input.Actions.SimpleAttack.started -= OnSimpleAttackPressed;
            _input.Actions.SimpleAttack.canceled -= OnSimpleAttackCanceled;

            _input.Disable();
        }

        private void OnMoveLeftButtonPressed(InputAction.CallbackContext callbackContext)
        {
            _moveLeftButtonPressedHashed = true;
            _moveRightButtonPressedHashed = false;

            NotifyInternal();
        }

        private void OnMoveLeftButtonCanceled(InputAction.CallbackContext callbackContext)
        {
            _moveLeftButtonPressedHashed = false;

            NotifyInternal();
        }

        private void OnMoveRightButtonPressed(InputAction.CallbackContext callbackContext)
        {
            _moveRightButtonPressedHashed = true;
            _moveLeftButtonPressedHashed = false;

            NotifyInternal();
        }

        private void OnMoveRightButtonCanceled(InputAction.CallbackContext callbackContext)
        {
            _moveRightButtonPressedHashed = false;

            NotifyInternal();
        }

        private void OnMoveBoostButtonPressed(InputAction.CallbackContext callbackContext)
        {
            _moveBoostButtonPressedHashed = true;

            NotifyInternal();
        }

        private void OnMoveBoostButtonCanceled(InputAction.CallbackContext callbackContext)
        {
            _moveBoostButtonPressedHashed = false;

            NotifyInternal();
        }

        private void OnJumpButtonPressed(InputAction.CallbackContext callbackContext)
        {
            _jumpButtonPressedHashed = true;

            NotifyInternal();
        }

        private void OnJumpButtonCanceled(InputAction.CallbackContext callbackContext)
        {
            _jumpButtonPressedHashed = false;

            NotifyInternal();
        }

        private void OnSimpleAttackPressed(InputAction.CallbackContext callbackContext)
        {
            _simpleAttackButtonPressedHashed = true;

            NotifyInternal();
        }

        private void OnSimpleAttackCanceled(InputAction.CallbackContext callbackContext)
        {
            _simpleAttackButtonPressedHashed = false;

            NotifyInternal();
        }

        private void NotifyInternal()
        {
            Notify(new InputData
            {
                MoveLeftButtonPressed = _moveLeftButtonPressedHashed,
                MoveRightButtonPressed = _moveRightButtonPressedHashed,
                MoveBoostButtonPressed = _moveBoostButtonPressedHashed,
                JumpButtonPressed = _jumpButtonPressedHashed,
                SimpleAttackButtonPressed = _simpleAttackButtonPressedHashed
            });
        }

        private readonly MainPlayerInput _input;

        private bool _moveLeftButtonPressedHashed;
        private bool _moveRightButtonPressedHashed;
        private bool _moveBoostButtonPressedHashed;
        private bool _jumpButtonPressedHashed;
        private bool _simpleAttackButtonPressedHashed;
    }
}
