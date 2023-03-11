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
            DataComponent dataComponent,
            StaffEffectsComponent staffEffectsComponent,
            CursorCaptureComponent cursorCaptureComponent,
            CameraCaptureComponent cameraCaptureComponent)
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
            _staffEffectsComponent = staffEffectsComponent.IfNullThrowExceptionOrReturn();
            _cursorCaptureComponent = cursorCaptureComponent.IfNullThrowExceptionOrReturn();
            _cameraCaptureComponent = cameraCaptureComponent.IfNullThrowExceptionOrReturn();
        }

        [SerializeField] private Transform _bottomRightRayPoint;
        [SerializeField] private Transform _bottomLeftRayPoint;
        [SerializeField] private Transform _topLeftRayPoint;
        [SerializeField] private Transform _topRightRayPoint;
        [SerializeField] private Transform _staffEffectsPoint;

        private void OnEnable()
        {
            ActivateAllComponents();
        }

        private void OnDisable()
        {
            DeactivateAllComponents();
        }

        private void Awake()
        {
            CheckSerializeFieldsOnNull();
            StartAllComponents();
        }

        private void Start()
        {
            _cameraCaptureComponent.Start(this);
        }

        private void ActivateAllComponents()
        {
            _inputComponent.Activate();
            _animationComponent.Activate();
            _jumpComponent.Activate();
            _groundAndWallCheckerComponent.Activate();
            _flipComponent.Activate();
            _moveBoostComponent.Activate();
            _moveComponent.Activate();
            _fallTrackingComponent.Activate();
            _dataComponent.Activate();
            _cursorCaptureComponent.Activate();
            _attackComponent.Activate();
            _stateComponent.Activate();
            _staffEffectsComponent.Activate();
            _cameraCaptureComponent.Activate();
        }

        private void DeactivateAllComponents()
        {
            _inputComponent.Deactivate();
            _animationComponent.Deactivate();
            _jumpComponent.Deactivate();
            _groundAndWallCheckerComponent.Deactivate();
            _flipComponent.Deactivate();
            _moveBoostComponent.Deactivate();
            _moveComponent.Deactivate();
            _fallTrackingComponent.Deactivate();
            _dataComponent.Deactivate();
            _cursorCaptureComponent.Deactivate();
            _attackComponent.Deactivate();
            _stateComponent.Deactivate();
            _staffEffectsComponent.Deactivate();
            _cameraCaptureComponent.Deactivate();
        }

        private void CheckSerializeFieldsOnNull()
        {
            _bottomRightRayPoint.IfNullThrowException();
            _bottomLeftRayPoint.IfNullThrowException();
            _topLeftRayPoint.IfNullThrowException();
            _topRightRayPoint.IfNullThrowException();
            _staffEffectsPoint.IfNullThrowException();
        }

        private void StartAllComponents()
        {
            _groundAndWallCheckerComponent.Start(
                _bottomRightRayPoint,
                _bottomLeftRayPoint,
                _topLeftRayPoint,
                _topRightRayPoint);
            _staffEffectsComponent.Start(_staffEffectsPoint);
            _inputComponent.Start(this);
            _animationComponent.Start(this);
            _cursorCaptureComponent.Start(this);
            _attackComponent.Start(_staffEffectsPoint);
            _stateComponent.Start(this);
            _jumpComponent.Start(this);
            _flipComponent.Start(this);
            _moveBoostComponent.Start(this);
            _moveComponent.Start(this);
            _fallTrackingComponent.Start(this);
            _dataComponent.Start(this);
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
        private StaffEffectsComponent _staffEffectsComponent;
        private CursorCaptureComponent _cursorCaptureComponent;
        private CameraCaptureComponent _cameraCaptureComponent;
    }
}