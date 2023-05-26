using System.Collections.Generic;
using System.IO;

namespace TunicReplayPlugin
{
    internal class ReplayDatabase
    {
        public static ReplayDatabase Instance { get; set; }

        private const string ReplayFilePattern = "*.trp";

        private readonly string replaysDir;
        private readonly List<Replay> replays;

        private List<int> sceneIndexes;
        private float segmentStartGameTime;

        public ReplayDatabase(string replaysDir)
        {
            this.replaysDir = replaysDir;
            this.replays = new List<Replay>();
            this.Mode = ReplayMode.BestTimeInSegment;
        }

        public enum ReplayMode
        {
            TimeInGame,
            TimeInSegment,
            BestTimeInSegment
        }

        public ReplayMode Mode { get; set; }

        public void BeginSegment(List<int> sceneIndexes, float gameTime)
        {
            this.sceneIndexes = sceneIndexes;
            this.segmentStartGameTime = gameTime;
        }

        public List<ReplayInstant> AtTime(int sceneIndex, float gameTime)
        {
            var instants = new List<ReplayInstant>();
            switch (this.Mode)
            {
                case ReplayMode.TimeInGame:

                    foreach (var replay in this.replays)
                    {
                        var instant = replay.AtTime(gameTime);
                        if (instant != null)
                        {
                            instants.Add(instant);
                        }
                    }

                    break;

                case ReplayMode.TimeInSegment:
                    GetInstantsAtSegment(gameTime, instants);
                    break;

                case ReplayMode.BestTimeInSegment:

                    ReplaySegment bestCompleteSegment = null;
                    foreach (var replay in this.replays)
                    {
                        var segment = replay.GetSegment(sceneIndexes);
                        if (segment != null && segment.NextSegment != null &&
                            (bestCompleteSegment == null || segment.Duration < bestCompleteSegment.Duration))
                        {
                            bestCompleteSegment = segment;
                        }
                    }

                    if (bestCompleteSegment != null)
                    {
                        var instant = bestCompleteSegment.AtTime(gameTime - this.segmentStartGameTime);
                        if (instant != null)
                        {
                            instants.Add(instant);
                        }
                    }
                    else
                    {
                        // Couldn't find a complete segment, so show all segments
                        GetInstantsAtSegment(gameTime, instants);
                    }

                    break;
            }

            instants.RemoveAll(instant => instant.SceneIndex != sceneIndex);

            return instants;
        }

        private void GetInstantsAtSegment(float gameTime, List<ReplayInstant> instants)
        {
            foreach (var replay in this.replays)
            {
                var segment = replay.GetSegment(sceneIndexes);
                if (segment != null)
                {
                    var instant = segment.AtTime(gameTime - this.segmentStartGameTime);
                    if (instant != null)
                    {
                        instants.Add(instant);
                    }
                }
            }
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
