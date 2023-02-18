using Assets.Scripts.Player.Components.Base;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [Inject]
        private void Construct(
            [Inject(Id = "inputComponent")] ComponentBase inputComponent,
            [Inject(Id = "animationComponent")] ComponentBase animationComponent,
            [Inject(Id = "stateComponent")] ComponentBase stateComponent,
            [Inject(Id = "jumpComponent")] ComponentBase jumpComponent)
        {
            _inputComponent = inputComponent;
            _animationComponent = animationComponent;
            _stateComponent = stateComponent;
            _jumpComponent = jumpComponent;
        }

        private void OnEnable()
        {
            _inputComponent.Activate();
            _animationComponent.Activate();
            _stateComponent.Activate();
            _jumpComponent.Activate();
        }

        private void OnDisable()
        {
            _inputComponent.Deactivate();
            _animationComponent.Deactivate();
            _stateComponent.Deactivate();
            _jumpComponent.Deactivate();
        }

        private ComponentBase _inputComponent;
        private ComponentBase _animationComponent;
        private ComponentBase _stateComponent;
        private ComponentBase _jumpComponent;
    }
}

