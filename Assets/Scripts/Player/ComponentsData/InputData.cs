using Assets.Scripts.Player.ComponentsData.Interfaces;

namespace Assets.Scripts.Player.ComponentsData
{
    public struct InputData : IData
    {
        public bool MoveLeftButtonPressed{ get; set; }

        public bool MoveRightButtonPressed { get; set; }

        public bool MoveBoostButtonPressed { get; set; }

        public bool JumpButtonPressed { get; set; }

        public bool SimpleAttackButtonPressed { get; set; }
    }
}