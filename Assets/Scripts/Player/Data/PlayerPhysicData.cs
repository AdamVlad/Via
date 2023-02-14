namespace Assets.Scripts.Player.Data
{
    public class PlayerPhysicData : DataBase
    {
        private bool _falling;
        public bool Falling
        {
            get => _falling;
            set
            {
                if (_falling == value) return;

                _falling = value;
                OnDataChanged();
            }
        }
    }
}