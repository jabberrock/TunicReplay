using System.Collections.Generic;
using UnityEngine;

namespace TunicReplayPlugin
{
    internal class ReplaySegment
    {
        public int Player { get; set; }
        public int SceneIndex { get; set; }
        public float StartReplayGameTime { get; set; }

        public List<ReplayInstant> Instants = new List<ReplayInstant>();

        public ReplayInstant Interpolate(float gameTimeInSegment)
        {
            if (this.Instants.Count == 0)
            {
                return null;
            }

            var replayGameTime = gameTimeInSegment + this.StartReplayGameTime;

            var searchIndex = this.Instants.BinarySearch(new ReplayInstant() { ReplayGameTime = replayGameTime });
            if (searchIndex >= 0)
            {
                return this.Instants[searchIndex];
            }

            var nextIndex = ~searchIndex;
            if (nextIndex == 0)
            {
                return this.Instants[0];
            }
            else if (nextIndex == this.Instants.Count)
            {
                return this.Instants[this.Instants.Count - 1];
            }
            else
            {
                var previous = this.Instants[nextIndex - 1];
                var next = this.Instants[nextIndex];
                var t = (replayGameTime - previous.ReplayGameTime) / (next.ReplayGameTime - previous.ReplayGameTime);

                return new ReplayInstant()
                {
                    Player = this.Player,
                    ReplayGameTime = gameTimeInSegment + this.StartReplayGameTime,
                    Position = Vector3.Slerp(previous.Position, next.Position, t),
                    Rotation = Quaternion.Slerp(previous.Rotation, next.Rotation, t)
                };
            }
        }
    }
}
