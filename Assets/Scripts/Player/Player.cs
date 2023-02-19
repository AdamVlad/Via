using Assets.Scripts.Extensions;
using Assets.Scripts.Player.Components;
using UnityEngine;
using Zenject;

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
            GroundAndWallCheckerComponent groundAndWallCheckerComponent)
        {
            _inputComponent = inputComponent;
            _animationComponent = animationComponent;
            _stateComponent = stateComponent;
            _jumpComponent = jumpComponent;
            _groundAndWallCheckerComponent = groundAndWallCheckerComponent;
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
        }

        private void OnDisable()
        {
            _inputComponent.Deactivate();
            _animationComponent.Deactivate();
            _stateComponent.Deactivate();
            _jumpComponent.Deactivate();
            _groundAndWallCheckerComponent.Deactivate();
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
    }
}

