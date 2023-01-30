namespace Assets.Scripts.Player
{
    public interface IInputComponent
    {
        void Start();
        void Destroy();
        void InputOn();
        void InputOff();
    }
}
