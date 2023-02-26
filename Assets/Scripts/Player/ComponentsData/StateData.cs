using Assets.Scripts.Player.ComponentsData.Interfaces;

namespace Assets.Scripts.Player.ComponentsData
{
    public struct StateData : IData
    {
        public bool IsFalling { get; set; }

        public bool IsOnGround { get; set; }

        public bool TouchedWall { get; set; }

        public bool MoveLeftButtonPressed { get; set; }

        public bool MoveRightButtonPressed { get; set; }

        public bool MoveBoostButtonPressed { get; set; }

        public bool JumpButtonPressed { get; set; }

        public bool SimpleAttackButtonPressed { get; set; }

        public float BoostMultiplier { get; set; }
    }
}