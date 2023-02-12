using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;
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
            PlayerInputData inputData,
            PlayerCollisionData collisionData)
        {
            _inputComponent = inputComponent;
            _collisionComponent = collisionComponent;
            _stateComponent = stateComponent;

            _inputData = inputData;
            _collisionData = collisionData;
        }

        private void OnEnable()
        {
            _inputComponent?.InputOn();
        }

        private void Start()
        {
            _inputComponent.Start();
            _inputComponent.InputOn();

            _stateComponent.Start();
        }

        private void FixedUpdate()
        {
            _stateComponent.FixedUpdate(ref _inputData, ref _collisionData);
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
        }

        private void OnDestroy()
        {
            _inputComponent.Destroy();
        }

        private IInputComponent _inputComponent;
        private ICollisionComponent _collisionComponent;
        private IStateComponent _stateComponent;

        private PlayerInputData _inputData;
        private PlayerCollisionData _collisionData;
    }
}

