using Assets.Scripts.Player.ComponentsData.Interfaces;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

namespace Assets.Scripts.Player.ComponentsData
{
    public struct CursorData : IData
    {
        public Vector3 Coordinates { get; set; }
    }
}