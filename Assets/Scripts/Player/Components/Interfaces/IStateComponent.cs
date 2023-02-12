using Assets.Scripts.Player.Data;

namespace Assets.Scripts.Player.Components.Interfaces
{
    public interface IStateComponent
    {
        void Start();
        void FixedUpdate(ref PlayerInputData inputData, ref PlayerCollisionData collisionData);
        void Update();
    }
}

