using Assets.Scripts.Player.ComponentsData;

namespace Assets.Scripts.Patterns.Observer
{
    public interface IObserver
    {
        void Update(ref IData data);
    }
}