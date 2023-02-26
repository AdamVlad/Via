using UnityEngine;
using Zenject;

using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [Inject]
        private void Construct(
            InputComponent inputComponent,
            AnimationComponent animationComponent,
            StateComponent stateComponent,
            JumpComponent jumpComponent,
            GroundAndWallCheckerComponent groundAndWallCheckerComponent,
            FlipComponent flipComponent,
            MoveComponent moveComponent,
            FallTrackingComponent fallTrackingComponent,
            MoveBoostComponent moveBoostComponent,
            AttackComponent attackComponent,
            DataComponent dataComponent)
        {
            _inputComponent = inputComponent.IfNullThrowExceptionOrReturn();
            _animationComponent = animationComponent.IfNullThrowExceptionOrReturn();
            _stateComponent = stateComponent.IfNullThrowExceptionOrReturn();
            _jumpComponent = jumpComponent.IfNullThrowExceptionOrReturn();
            _groundAndWallCheckerComponent = groundAndWallCheckerComponent.IfNullThrowExceptionOrReturn();
            _flipComponent = flipComponent.IfNullThrowExceptionOrReturn();
            _moveComponent = moveComponent.IfNullThrowExceptionOrReturn();
            _fallTrackingComponent = fallTrackingComponent.IfNullThrowExceptionOrReturn();
            _moveBoostComponent = moveBoostComponent.IfNullThrowExceptionOrReturn();
            _attackComponent = attackComponent.IfNullThrowExceptionOrReturn();
            _dataComponent = dataComponent.IfNullThrowExceptionOrReturn();
        }

        [SerializeField] private Transform _bottomRightRayPoint;
        [SerializeField] private Transform _bottomLeftRayPoint;
        [SerializeField] private Transform _topLeftRayPoint;
        [SerializeField] private Transform _topRightRayPoint;

        private void OnEnable()
        {
            _inputComponent.Activate();
            _animationComponent.Activate();
            _stateComponent.Activate();
            _jumpComponent.Activate();
            _groundAndWallCheckerComponent.Activate();
            _flipComponent.Activate();
            _moveBoostComponent.Activate();
            _moveComponent.Activate();
            _fallTrackingComponent.Activate();
            _attackComponent.Activate();
            _dataComponent.Activate();
        }

        private void OnDisable()
        {
            _inputComponent.Deactivate();
            _animationComponent.Deactivate();
            _stateComponent.Deactivate();
            _jumpComponent.Deactivate();
            _groundAndWallCheckerComponent.Deactivate();
            _flipComponent.Deactivate();
            _moveBoostComponent.Deactivate();
            _moveComponent.Deactivate();
            _fallTrackingComponent.Deactivate();
            _attackComponent.Deactivate();
            _dataComponent.Deactivate();
        }

        private void Awake()
        {
            _bottomRightRayPoint.IfNullThrowException();
            _bottomLeftRayPoint.IfNullThrowException();
            _topLeftRayPoint.IfNullThrowException();
            _topRightRayPoint.IfNullThrowException();
        }

        private void Start()
        {
            _groundAndWallCheckerComponent.Start(
                _bottomRightRayPoint, 
                _bottomLeftRayPoint, 
                _topLeftRayPoint, 
                _topRightRayPoint);
        }

        private InputComponent _inputComponent;
        private AnimationComponent _animationComponent;
        private StateComponent _stateComponent;
        private JumpComponent _jumpComponent;
        private GroundAndWallCheckerComponent _groundAndWallCheckerComponent;
        private FlipComponent _flipComponent;
        private MoveComponent _moveComponent;
        private FallTrackingComponent _fallTrackingComponent;
        private MoveBoostComponent _moveBoostComponent;
        private AttackComponent _attackComponent;
        private DataComponent _dataComponent;
    }
}

