using Assets.Configs.InputSystem;
using Assets.Scripts.Player.Components;
using Assets.Scripts.Player.Data;
using Assets.Scripts.Player.States;

using DragonBones;
using UnityEngine;

namespace Assets.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : MonoBehaviour
    {
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _armature = GetComponentInChildren<UnityArmatureComponent>();
        }

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
            _stateMachine.ChangeState(_inputData.WalkButtonPressed ? _walkState : _idleState);

            _stateMachine.CurrentState.FixedUpdate();
        }

        private void Update()
        {
            _stateMachine.CurrentState.Update();
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

            _stateMachine = new StateMachine();
            _idleState = new IdleState(gameObject, _stateMachine);
            _walkState = new WalkState(gameObject, _stateMachine, ref _inputData);
            _stateMachine.Initialize(_idleState);

            _inputComponent = new InputComponent(new MainPlayerInput(), ref _inputData);
        }

        private IInputComponent _inputComponent;

        private StateMachine _stateMachine;
        private StateBase _idleState, _walkState;

        private PlayerInputData _inputData;

        private Rigidbody2D _rigidbody;
        private UnityArmatureComponent _armature;
    }
}

