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
            ICollisionComponent collisionComponent,
            IStateComponent stateComponent,
            IPhysicComponent physicComponent)
        {
            _inputComponent = inputComponent;
            _collisionComponent = collisionComponent;
            _stateComponent = stateComponent;
            _physicComponent = physicComponent;
        }

        private void OnEnable()
        {
            _inputComponent?.InputOn();
            _stateComponent.OnEnable();
        }

        private void Start()
        {
            _inputComponent.Start();
            _inputComponent.InputOn();
            _stateComponent.Start();
        }

        private void FixedUpdate()
        {
            _physicComponent.FixedUpdate();
            _stateComponent.FixedUpdate();
        }

        private void Update()
        {
            _stateComponent.Update();
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
            _stateComponent?.OnDisable();
        }

        private void OnDestroy()
        {
            _inputComponent.Destroy();
        }

        private IInputComponent _inputComponent;
        private ICollisionComponent _collisionComponent;
        private IStateComponent _stateComponent;
        private IPhysicComponent _physicComponent;
    }
}

