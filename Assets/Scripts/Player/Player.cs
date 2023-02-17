using Assets.Scripts.Player.Components.Interfaces;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        [Inject]
        private void Construct(
            IInputComponent inputComponent,
            IAnimationComponent animationComponent,
            ICollisionComponent collisionComponent,
            IStateComponent stateComponent,
            IPhysicComponent physicComponent)
        {
            _inputComponent = inputComponent;
            _animationComponent = animationComponent;
            _collisionComponent = collisionComponent;
            _stateComponent = stateComponent;
            _physicComponent = physicComponent;
        }

        private void OnEnable()
        {
            _inputComponent?.InputOn();
            _animationComponent.OnEnable();
            _stateComponent.OnEnable();
            _physicComponent.OnEnable();
        }

        private void Start()
        {
            _inputComponent.Start();
            _inputComponent.InputOn();  
        }

        private void FixedUpdate()
        {
            _physicComponent.FixedUpdate();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            _collisionComponent.OnTriggerEnter2D(collision);
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            _collisionComponent.OnTriggerExit2D(collision);
        }

        private void OnDisable()
        {
            _inputComponent?.InputOff();
            _animationComponent.OnDisable();
            _stateComponent.OnDisable();
            _physicComponent.OnDisable();
        }

        private void OnDestroy()
        {
            _inputComponent.Destroy();
        }
        
        private IInputComponent _inputComponent;
        private IAnimationComponent _animationComponent;
        private IStateComponent _stateComponent;
        private ICollisionComponent _collisionComponent;
        private IPhysicComponent _physicComponent;
    }
}

