using Assets.Scripts.Player.ComponentsData.Interfaces;

namespace Assets.Scripts.Utils.Observer
{
    public interface IObserver
    {
        void Update(ref IData data);
    }
}