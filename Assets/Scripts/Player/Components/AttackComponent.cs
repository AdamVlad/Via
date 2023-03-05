using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Utils.EventBus;

namespace Assets.Scripts.Player.Components
{
    public class AttackComponent : ComponentBase
    {
        public AttackComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings) : base(eventBus, settings)
        {
        }
    }
}