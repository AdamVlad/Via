namespace Assets.Scripts.Player.Data
{
    public class PlayerCollisionData : DataBase
    {
        private bool _onGround;
        public bool OnGround
        {
            get => _onGround;
            set
            {
                if (_onGround == value) return;

                _onGround = value;
                OnDataChanged();
            }
        }
    }
}

