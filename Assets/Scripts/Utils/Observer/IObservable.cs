using Assets.Scripts.Player.ComponentsData.Interfaces;

namespace Assets.Scripts.Utils.Observer
{
    public interface IObservable
    {
        void AddObserver(IObserver observer);

        void RemoveObserver(IObserver observer);

        void Notify(ref IData data);
    }
}