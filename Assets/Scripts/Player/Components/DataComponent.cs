using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components.Base;
using Assets.Scripts.Player.ComponentsData;
using Assets.Scripts.Player.ComponentsData.Interfaces;
using Assets.Scripts.Utils.EventBus;
using Assets.Scripts.Utils.Observer;

namespace Assets.Scripts.Player.Components
{
    public class DataComponent : ObservableComponentDecorator, IObserver
    {
        public DataComponent(
            IEventBus<PlayerEvents> eventBus,
            PlayerSettings settings,
            InputComponent inputComponent,
            GroundAndWallCheckerComponent groundAndWallCheckerObservable,
            FallTrackingComponent fallTrackingComponent,
            AnimationComponent animationComponent) : base(eventBus, settings)
        {
            _inputObservable = inputComponent.IfNullThrowExceptionOrReturn();
            _groundAndWallCheckerObservable = groundAndWallCheckerObservable.IfNullThrowExceptionOrReturn();
            _fallObservable = fallTrackingComponent.IfNullThrowExceptionOrReturn();
            _animationObservable = animationComponent.IfNullThrowExceptionOrReturn();

            InputDataHashed = new InputData();
            GroundAndWallDataHashed = new GroundAndWallCheckerData();
            FallingDataHashed = new FallingData();
            AttackDataHashed = new AttackData();
        }

        public InputData InputDataHashed;
        public GroundAndWallCheckerData GroundAndWallDataHashed;
        public FallingData FallingDataHashed;
        public AttackData AttackDataHashed;

        protected override void ActivateInternal()
        {
            base.ActivateInternal();

            _inputObservable.AddObserver(this);
            _groundAndWallCheckerObservable.AddObserver(this);
            _fallObservable.AddObserver(this);
            _animationObservable.AddObserver(this);
        }

        protected override void DeactivateInternal()
        {
            base.DeactivateInternal();

            _inputObservable.RemoveObserver(this);
            _groundAndWallCheckerObservable.RemoveObserver(this);
            _fallObservable.RemoveObserver(this);
            _animationObservable.RemoveObserver(this);
        }

        public void Update(ref IData data)
        {
            switch (data)
            {
                case InputData inputData:

                    InputDataHashed = inputData;
                    Notify(InputDataHashed);
                    break;

                case GroundAndWallCheckerData groundAndWallCheckerData:

                    GroundAndWallDataHashed = groundAndWallCheckerData;
                    Notify(GroundAndWallDataHashed);
                    break;

                case FallingData fallingData:
                    FallingDataHashed = fallingData;
                    Notify(FallingDataHashed);
                    break;

                case AttackData attackData:

                    AttackDataHashed = attackData;
                    Notify(AttackDataHashed);
                    AttackDataHashed.IsSimpleAttackEnded = false;
                    break;
            }
        }

        private readonly ObservableComponentDecorator _inputObservable;
        private readonly ObservableComponentDecorator _groundAndWallCheckerObservable;
        private readonly ObservableComponentDecorator _fallObservable;
        private readonly ObservableComponentDecorator _animationObservable;
    }
}