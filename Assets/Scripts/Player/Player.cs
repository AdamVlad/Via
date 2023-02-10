using Assets.Scripts.Player.Components;
using Assets.Scripts.Player.Components.Interfaces;
using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.Settings;
using Assets.Scripts.Player.States;
using Assets.Scripts.Player.States.Concrete;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private void OnEnable()
        {
            _inputComponent?.InputOn();
        }

        private void Start()
        {
            InitializeComponents();

            _inputComponent.Start();
            _inputComponent.InputOn();
        }

        private void FixedUpdate()
        {
            if (_inputData.JumpButtonPressed && _collisionData.OnGround)
            {
                _stateMachine.ChangeState(_jumpStartState);
            }
            if (_collisionData.Flying)
            {
                _stateMachine.ChangeState(_flyingState);
            }
            if (_inputData.WalkButtonPressed)
            {
                _stateMachine.ChangeState(_walkState);
            }
            else if (_collisionData.OnGround)
            {
                _stateMachine.ChangeState(_idleState);
            }

            _stateMachine.CurrentState.FixedUpdate();
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
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

        private void InitializeComponents()
        {
            _inputData = new PlayerInputData();
            _collisionData = new PlayerCollisionData();

            _stateMachine = new StateMachine();

            _idleState = new IdleState(gameObject, _stateMachine, _settings, ref _collisionData);
            _walkState = new WalkState(gameObject, _stateMachine, _settings,  ref _inputData, ref _collisionData);
            _jumpStartState = new JumpStartState(gameObject, _stateMachine, _settings,  ref _collisionData);
            _flyingState = new FlyingState(gameObject, _stateMachine, _settings);

            _stateMachine.Initialize(_idleState);

            _inputComponent = new InputComponent(new MainPlayerInput(), ref _inputData);
            _collisionComponent = new CollisionComponent(ref _collisionData);
        }

        private IInputComponent _inputComponent;
        private ICollisionComponent _collisionComponent;

        private StateMachine _stateMachine;
        private StateBase _idleState, _walkState, _jumpStartState, _flyingState;

        private PlayerInputData _inputData;
        private PlayerCollisionData _collisionData;

        [Inject] private PlayerSettings _settings;
    }
}

