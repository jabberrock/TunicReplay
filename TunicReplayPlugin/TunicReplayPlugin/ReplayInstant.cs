using System;
using UnityEngine;

namespace TunicReplayPlugin
{
    internal class ReplayInstant : IComparable<ReplayInstant>
    {
        public int Player { get; set; }
        public float ReplayGameTime { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public int CompareTo(ReplayInstant other)
        {
            return Math.Sign(this.ReplayGameTime - other.ReplayGameTime);
        }
    }
}
