using Assets.Scripts.Player.ComponentsData;

namespace Assets.Scripts.Patterns.Observer
{
    public interface IObservable
    {
        void AddObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void Notify(ref IData data);
    }
}