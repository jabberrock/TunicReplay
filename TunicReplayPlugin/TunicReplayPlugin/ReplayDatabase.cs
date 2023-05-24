using System.Collections.Generic;
using System.IO;

namespace TunicReplayPlugin
{
    internal class ReplayDatabase
    {
        private const string ReplayFilePattern = "*.trp";

        private readonly string replaysDir;
        private readonly List<Replay> replays;

        private List<ReplaySegment> segments;
        private float segmentStartGameTime;

        public ReplayDatabase(string replaysDir)
        {
            this.replaysDir = replaysDir;
            this.replays = new List<Replay>();
        }

        public void JumpToSegment(List<int> sceneIndexes, float gameTime)
        {
            this.segments = new List<ReplaySegment>();
            this.segmentStartGameTime = 0.0f;

            foreach (var replay in this.replays)
            {
                var segment = replay.GetSegment(sceneIndexes);
                if (segment != null)
                {
                    this.segments.Add(segment);
                }
            }

            this.segmentStartGameTime = gameTime;

            Logger.LogInfo("Jump to segment " + string.Join(",", sceneIndexes) + " at game time " + gameTime + " found " + this.segments.Count + " segments");
        }

        public List<ReplayInstant> At(float gameTime)
        {
            var instants = new List<ReplayInstant>();
            foreach (var segment in this.segments)
            {
                var instant = segment.Interpolate(gameTime - segmentStartGameTime);
                if (instant != null)
                {
                    instants.Add(instant);
                }
            }

            // Logger.LogInfo("Found " + instants.Count + " instants at " + gameTime);

            return instants;
        }

        public void LoadReplays()
        {
            this.replays.Clear();

            var replayPaths = Directory.GetFiles(replaysDir, ReplayFilePattern);
            for (int i = 0; i < replayPaths.Length; ++i)
            {
                try
                {
                    var replay = new Replay(i, replayPaths[i]);
                    this.replays.Add(replay);
                }
                catch (IOException e)
                {
                    Logger.LogWarning("Failed to load replay " + replayPaths[i] + ": " + e.Message);
                }
            }

            Logger.LogInfo("Loaded " + this.replays.Count + " replays");
        }
    }
}
