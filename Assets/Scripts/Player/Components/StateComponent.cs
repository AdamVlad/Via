using Assets.Scripts.Patterns;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Patterns.Observer;
using Zenject;

namespace Assets.Scripts.Player.Components
{
    public sealed class StateComponent : ComponentBase, IObserver
    {
        public StateComponent(
            IEventBus<PlayerStates> eventBus,
            [Inject(Id = "inputComponent")] ComponentBase inputComponent) : base(eventBus)
        {
            _observable = inputComponent as ObservableComponentDecorator;
        }

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _observable.AddObserver(this);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _observable.RemoveObserver(this);
        }

        public void Update(ref IData data)
        {
            if (data is InputData inputData)
            {
                ChangeState(ref inputData);
            }
        }

        private void ChangeState(ref InputData inputData)
        {
            if (inputData.JumpButtonPressed)
            {
                _eventBus.RaiseEvent(PlayerStates.JumpStart);
                return;
            }
            if (inputData.MoveLeftButtonPressed)
            {
                _eventBus.RaiseEvent(PlayerStates.MoveLeft);
                return;
            }
            if (inputData.MoveRightButtonPressed)
            {
                _eventBus.RaiseEvent(PlayerStates.MoveRight);
                return;
            }

            if (!inputData.MoveLeftButtonPressed &&
                !inputData.MoveRightButtonPressed &&
                !inputData.JumpButtonPressed)
            {
                _eventBus.RaiseEvent(PlayerStates.Idle);
            }
        }

        private readonly ObservableComponentDecorator _observable;
    }
}