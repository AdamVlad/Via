using Assets.Scripts.Patterns.EventBus;
using Assets.Scripts.Player.Components.Base;

namespace Assets.Scripts.Player.Components
{
    public class AttackComponent : ComponentBase
    {
        public AttackComponent(IEventBus<PlayerStates> eventBus) : base(eventBus)
        {
        }
    }
}