using UnityEngine;

namespace Assets.Scripts.Player
{
    [CreateAssetMenu(fileName = "Settings", menuName = "Player/Settings")]
    public class PlayerSettings : ScriptableObject
    {
        #region Physic

        [Header("Physic")]
        [Space]

        [SerializeField] private float _normalSpeed;
        public float NormalSpeed => _normalSpeed;

        [SerializeField, Range(1,3)] private float _boostSpeedMultiplier;
        public float BoostSpeedMultiplier => _boostSpeedMultiplier;

        [SerializeField] private float _jumpForce;
        public float JumpForce => _jumpForce;

        [Space]

        #endregion

        #region Collisions

        [Header("Collisions")]
        [Space]

        [SerializeField, Range(0,5)] private float _topRayLength;
        public float TopRayLength => _topRayLength;

        [SerializeField, Range(0, 5)] private float _bottomRayLength;
        public float BottomRayLength => _bottomRayLength;

        [SerializeField, Range(0, 5)] private float _leftRayLength;
        public float LeftRayLength => _leftRayLength;

        [SerializeField, Range(0, 5)] private float _rightRayLength;
        public float RightRayLength => _rightRayLength;

        [Space]

        #endregion

        #region Animation

        [Header("Animation")]
        [Space]

        [SerializeField] private string _idleAnimationName;
        public string IdleAnimationName => _idleAnimationName;

        [SerializeField] private float _idleStateTransition;
        public float IdleStateTransition => _idleStateTransition;

        [SerializeField, Range(-2, 2)] private float _idleAnimationPlayingSpeed = 1;
        public float idleAnimationPlayingSpeed => _idleAnimationPlayingSpeed;

        [Space]

        [SerializeField] private string _walkAnimationName;
        public string WalkAnimationName => _walkAnimationName;

        [SerializeField] private float _walkStateTransition;
        public float WalkStateTransition => _walkStateTransition;

        [SerializeField, Range(-2, 2)] private float _walkAnimationPlayingSpeed = 1;
        public float walkAnimationPlayingSpeed => _walkAnimationPlayingSpeed;

        [Space]

        [SerializeField] private string _jumpStartAnimationName;
        public string JumpStartAnimationName => _jumpStartAnimationName;

        [SerializeField] private float _jumpStartStateTransition;
        public float JumpStartStateTransition => _jumpStartStateTransition;

        [SerializeField, Range(-2, 2)] private float _jumpStartAnimationPlayingSpeed = 1;
        public float jumpStartAnimationPlayingSpeed => _jumpStartAnimationPlayingSpeed;

        [Space]

        [SerializeField] private string _fallingAnimationName;
        public string FallingAnimationName => _fallingAnimationName;

        [SerializeField] private float _fallingStateTransition;
        public float FallingStateTransition => _fallingStateTransition;

        [SerializeField, Range(-2, 2)] private float _fallingAnimationPlayingSpeed = 1;
        public float fallingAnimationPlayingSpeed => _fallingAnimationPlayingSpeed;

        [Space]

        [SerializeField] private string _moveBoostingAnimationName;
        public string MoveBoostingAnimationName => _moveBoostingAnimationName;

        [SerializeField] private float _moveBoostingStateTransition;
        public float MoveBoostingStateTransition => _moveBoostingStateTransition;

        [SerializeField, Range(-2, 2)] private float _moveBoostingAnimationPlayingSpeed = 1;
        public float MoveBoostingAnimationPlayingSpeed => _moveBoostingAnimationPlayingSpeed;

        #endregion
    }
}
