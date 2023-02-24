namespace Assets.Scripts.Player
{
    public enum PlayerStates
    {
        Idle,
        MoveLeft,
        MoveRight,
        MoveLeftWhenFlying,
        MoveRightWhenFlying,
        MoveStopped,
        MoveLeftBoost,
        MoveRightBoost,
        MoveBoostStopped,
        JumpStart,
        JumpStartWhenBoosted,
        Fall,
        Flip,
        SimpleAttack
    }
}