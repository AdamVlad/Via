using Assets.Scripts.Player.ComponentsData.Interfaces;

namespace Assets.Scripts.Player.ComponentsData
{
    public struct GroundAndWallCheckerData : IData
    {
        public bool IsOnGround { get; set; }

        public bool TouchedWall { get; set; }
    }
}