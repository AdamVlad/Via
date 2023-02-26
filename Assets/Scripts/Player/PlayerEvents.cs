namespace Assets.Scripts.Player
{
    public enum PlayerEvents
    {
        OnIdleStateEnter,
        OnMoveLeftStateEnter,
        OnMoveRightStateEnter,

        OnMoveLeftWhenFallingStateEnter,
        OnMoveRightWhenFallingStateEnter,

        MoveLeftBoost,
        MoveRightBoost,
        MoveBoostStopped,

        OnJumpStartStateEnter,

        JumpStartWhenBoosted,

        OnFallStateEnter,

        Flip,
        SimpleAttackStart,
        SimpleAttackEnd,

        StopMove
    }
}