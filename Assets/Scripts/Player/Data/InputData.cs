namespace Assets.Scripts.Player.Data
{
    //public class InputData : DataBase
    //{
    //    public bool MoveLeftButtonPressed { get; set; }
    //    public bool JumpButtonPressed { get; set; }

    //    public bool WalkLeftButtonPressed { get; set; }

    //    public float AxisXPressedValue { get; set; }
    //}

    public class InputData : DataBase
    {
        private bool _moveLeftButtonPressed;
        public bool MoveLeftButtonPressed
        {
            get => _moveLeftButtonPressed;
            set
            {
                if (_moveLeftButtonPressed == value) return;

                _moveLeftButtonPressed = value;
                OnDataChanged();
            }
        }

        private bool _moveRightButtonPressed;
        public bool MoveRightButtonPressed
        {
            get => _moveRightButtonPressed;
            set
            {
                if (_moveRightButtonPressed == value) return;

                _moveRightButtonPressed = value;
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
    }
}