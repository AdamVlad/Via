namespace Assets.Scripts.Player.Components
{
    public interface IInputComponent
    {
        void Start();
        void Destroy();
        void InputOn();
        void InputOff();
    }
}
