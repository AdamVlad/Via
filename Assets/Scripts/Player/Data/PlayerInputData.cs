using TMPro;

namespace Assets.Scripts.Player.Data
{
    public class PlayerInputData : DataBase
    {
        private bool _walkButtonPressed;
        public bool WalkButtonPressed
        {
            get => _walkButtonPressed;
            set
            {
                if (_walkButtonPressed == value) return;

                _walkButtonPressed = value;
                OnDataChanged();
            }
        }

        private bool _jumpButtonPressed;
        public bool JumpButtonPressed
        {
            get => _jumpButtonPressed;
            set
            {
                if (_jumpButtonPressed == value) return;

                _jumpButtonPressed = value;
                OnDataChanged();
            }
        }

        private bool _walkLeftButtonPressed;
        public bool WalkLeftButtonPressed
        {
            get => _walkLeftButtonPressed;
            set
            {
                if (_walkLeftButtonPressed == value) return;

                _walkLeftButtonPressed = value;
                OnDataChanged();
            }
        }

        private float _axisXPressedValue;
        public float AxisXPressedValue
        {
            get => _axisXPressedValue;
            set
            {
                if (_axisXPressedValue == value) return;

                _axisXPressedValue = value;
                OnDataChanged();
            }
        }
    }
}