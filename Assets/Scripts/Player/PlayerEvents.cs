namespace Assets.Scripts.Player
{
    public enum PlayerEvents
    {
        OnIdleStateEnter,
        OnMoveLeftStateEnter,
        OnMoveRightStateEnter,
        OnMoveLeftWhenFallingStateEnter,
        OnMoveRightWhenFallingStateEnter,
        OnBoostedMoveLeftStateEnter,
        OnBoostedMoveRightStateEnter,
        OnStoppingMove,
        OnJumpStartStateEnter,
        OnJumpStartWhenBoostedStateEnter,
        OnFallStateEnter,
        OnFlipPlayerPicture,
        OnSimpleAttackStartStateEnter,
        OnSimpleAttackEndStateEnter,
        ShootingFireBullet
    }
}