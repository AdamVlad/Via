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
            _inputComponent = new InputComponent(
                new MainPlayerInput(),
                _armature,
                _rigidbody);
        }

        private void Start()
        {
            _inputComponent.Start();
        }

        private void OnEnable()
        {
            _inputComponent.InputOn();
        }

        private void OnDisable()
        {
            _inputComponent.InputOff();
        }

        private void OnDestroy()
        {
            _inputComponent.Destroy();
        }

        private IInputComponent _inputComponent;
        private Rigidbody2D _rigidbody;
        private UnityArmatureComponent _armature;
    }
}

