using UnityEngine;

namespace Assets.Scripts.Player
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

        [SerializeField, Range(-2, 2)] private float _idleStatePlayingSpeed = 1;
        public float IdleStatePlayingSpeed => _idleStatePlayingSpeed;

        [Space]

        [SerializeField] private string _walkAnimationName;
        public string WalkAnimationName => _walkAnimationName;

        [SerializeField] private float _walkStateTransition;
        public float WalkStateTransition => _walkStateTransition;

        [SerializeField, Range(-2, 2)] private float _walkStatePlayingSpeed = 1;
        public float WalkStatePlayingSpeed => _walkStatePlayingSpeed;

        [Space]

        [SerializeField] private string _jumpStartAnimationName;
        public string JumpStartAnimationName => _jumpStartAnimationName;

        [SerializeField] private float _jumpStartStateTransition;
        public float JumpStartStateTransition => _jumpStartStateTransition;

        [SerializeField, Range(-2, 2)] private float _jumpStartStatePlayingSpeed = 1;
        public float JumpStartStatePlayingSpeed => _jumpStartStatePlayingSpeed;

        [Space]

        [SerializeField] private string _flyingAnimationName;
        public string FlyingAnimationName => _flyingAnimationName;

        [SerializeField] private float _flyingStateTransition;
        public float FlyingStateTransition => _flyingStateTransition;

        [SerializeField, Range(-2, 2)] private float _flyingStatePlayingSpeed = 1;
        public float FlyingStatePlayingSpeed => _flyingStatePlayingSpeed;

        #endregion
    }
}
