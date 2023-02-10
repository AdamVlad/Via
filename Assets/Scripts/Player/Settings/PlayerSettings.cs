using UnityEngine;

namespace Assets.Scripts.Player.Settings
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Player/Settings")]
    public class PlayerSettings : ScriptableObject
    {
        #region Physic

        [Header("Physic")]
        [Space]

        [SerializeField] private float _maxSpeed;
        public float MaxSpeed => _maxSpeed;

        [SerializeField] private float _jumpForce;
        public float JumpForce => _jumpForce;

        [Space]

        #endregion

        #region Animation

        [Header("Animation")]
        [Space]

        [SerializeField] private string _idleAnimationName;
        public string IdleAnimationName => _idleAnimationName;

        [SerializeField] private float _idleStateTransition;
        public float IdleStateTransition => _idleStateTransition;

        [Space]

        [SerializeField] private string _walkAnimationName;
        public string WalkAnimationName => _walkAnimationName;

        [SerializeField] private float _walkStateTransition;
        public float WalkStateTransition => _walkStateTransition;

        [Space]

        [SerializeField] private string _jumpStartAnimationName;
        public string JumpStartAnimationName => _jumpStartAnimationName;

        [SerializeField] private float _jumpStartStateTransition;
        public float JumpStartStateTransition => _jumpStartStateTransition;

        [Space]

        [SerializeField] private string _flyingAnimationName;
        public string FlyingAnimationName => _flyingAnimationName;

        [SerializeField] private float _flyingStateTransition;
        public float FlyingStateTransition => _flyingStateTransition;

        #endregion
    }
}
