using System;

namespace Assets.Scripts.Player.Data
{
    public abstract class DataBase
    {
        public event Action DataChanged;

        protected virtual void OnDataChanged()
        {
            DataChanged?.Invoke();
        }
    }
}