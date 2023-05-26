using System.Collections.Generic;

namespace TunicReplayPlugin
{
    internal class ReplaySegment
    {
        public int Player { get; set; }
        public int SceneIndex { get; set; }
        public float StartReplayGameTime { get; set; }
        public ReplaySegment NextSegment { get; set; }

        public List<ReplayInstant> Instants = new List<ReplayInstant>();

        public float Duration
        {
            get
            {
                if (this.Instants.Count > 0)
                {
                    return this.Instants[this.Instants.Count - 1].ReplayGameTime - this.StartReplayGameTime;
                }
                else
                {
                    return float.MaxValue;
                }
            }
        }

        public ReplayInstant AtTime(float segmentGameTime)
        {
            return ReplayInstant.Interpolate(segmentGameTime + this.StartReplayGameTime, this.Instants);
        }
    }
}
