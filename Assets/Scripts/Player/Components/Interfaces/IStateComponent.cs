namespace Assets.Scripts.Player.Components.Interfaces
{
    public interface IStateComponent
    {
        void OnEnable();
        void OnDisable();
        void Start();
        void FixedUpdate();
        void Update();
    }
}

