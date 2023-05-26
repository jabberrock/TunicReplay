using System;
using System.Collections.Generic;
using UnityEngine;

namespace TunicReplayPlugin
{
    internal class ReplayInstant : IComparable<ReplayInstant>
    {
        public int Player { get; set; }
        public int SceneIndex { get; set; }
        public float ReplayGameTime { get; set; }
        public Vector3 Position { get; set; }
        public Quaternion Rotation { get; set; }

        public int CompareTo(ReplayInstant other)
        {
            return Math.Sign(this.ReplayGameTime - other.ReplayGameTime);
        }

        public static ReplayInstant Interpolate(float replayGameTime, List<ReplayInstant> instants)
        {
            if (instants.Count == 0)
            {
                return null;
            }

            var searchIndex = instants.BinarySearch(new ReplayInstant() { ReplayGameTime = replayGameTime });
            if (searchIndex >= 0)
            {
                return instants[searchIndex];
            }

            var nextIndex = ~searchIndex;
            if (nextIndex == 0)
            {
                return instants[0];
            }
            else if (nextIndex == instants.Count)
            {
                return instants[instants.Count - 1];
            }
            else
            {
                var previous = instants[nextIndex - 1];
                var next = instants[nextIndex];
                var t = (replayGameTime - previous.ReplayGameTime) / (next.ReplayGameTime - previous.ReplayGameTime);

                return new ReplayInstant()
                {
                    Player = previous.Player,
                    SceneIndex = previous.SceneIndex,
                    ReplayGameTime = replayGameTime,
                    Position = Vector3.Slerp(previous.Position, next.Position, t),
                    Rotation = Quaternion.Slerp(previous.Rotation, next.Rotation, t)
                };
            }
        }
    }
}
