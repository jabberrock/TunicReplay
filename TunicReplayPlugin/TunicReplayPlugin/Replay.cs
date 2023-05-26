using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TunicReplayPlugin
{
    internal class Replay
    {
        public Replay(int player, string replayPath)
        {
            this.Player = player; 
            this.Segments = new List<ReplaySegment>();
            this.Instants = new List<ReplayInstant>();

            LoadReplay(player, replayPath);

            Logger.LogInfo("Loaded replay at " + replayPath + " with " + this.Segments.Count + " segments");
        }

        public int Player { get; private set; }
        public List<ReplaySegment> Segments { get; private set; }
        public List<ReplayInstant> Instants { get; private set; }

        public ReplayInstant AtTime(float replayGameTime)
        {
            return ReplayInstant.Interpolate(replayGameTime, this.Instants);
        }

        public ReplaySegment GetSegment(List<int> sceneIndexes)
        {
            var segmentIndex = -1;
            foreach (var sceneIndex in sceneIndexes)
            {
                var nextSegmentIndex = segmentIndex + 1;

                // Allow runner to load into an area more times that the replay
                // TODO: Allow runner to load into an area fewer times than the replay
                // TODO: Wrong warp forces you to enter and exit an area multiple times... how?
                if (nextSegmentIndex < this.Segments.Count
                    && this.Segments[nextSegmentIndex].SceneIndex == sceneIndex)
                {
                    segmentIndex = nextSegmentIndex;
                }
            }

            // Make sure the segment matches the final requested scene
            if (segmentIndex >= 0 && this.Segments[segmentIndex].SceneIndex == sceneIndexes[sceneIndexes.Count - 1])
            {
                return this.Segments[segmentIndex];
            }

            return null;
        }

        private void LoadReplay(int player, string replayPath)
        {
            using (var binaryReader = new BinaryReader(File.OpenRead(replayPath)))
            {
                var versionHeader = binaryReader.ReadUInt32();
                if (versionHeader != 0x00001000)
                {
                    throw new IOException("Unsupported replay file version " + versionHeader.ToString("X8"));
                }

                while (true)
                {
                    try
                    {
                        var marker = binaryReader.ReadUInt32();
                        switch (marker)
                        {
                            case 0xDEADBEEF:
                                {
                                    var sceneIndex = binaryReader.ReadInt32();
                                    var startGameTime = binaryReader.ReadSingle();

                                    var segment = new ReplaySegment()
                                    {
                                        Player = player,
                                        SceneIndex = sceneIndex,
                                        StartReplayGameTime = startGameTime
                                    };

                                    if (this.Segments.Count > 0)
                                    {
                                        this.Segments[this.Segments.Count - 1].NextSegment = segment;
                                    }

                                    this.Segments.Add(segment);
                                }
                                break;

                            case 0xCAFED00D:
                                if (this.Segments.Count > 0)
                                {
                                    var segment = this.Segments[this.Segments.Count - 1];

                                    var gameTime = binaryReader.ReadSingle();
                                    var px = binaryReader.ReadSingle();
                                    var py = binaryReader.ReadSingle();
                                    var pz = binaryReader.ReadSingle();
                                    var rx = binaryReader.ReadSingle();
                                    var ry = binaryReader.ReadSingle();
                                    var rz = binaryReader.ReadSingle();
                                    var rw = binaryReader.ReadSingle();

                                    var instant = new ReplayInstant()
                                    {
                                        Player = player,
                                        SceneIndex = segment.SceneIndex,
                                        ReplayGameTime = gameTime,
                                        Position = new Vector3(px, py, pz),
                                        Rotation = new Quaternion(rx, ry, rz, rw)
                                    };
                                    segment.Instants.Add(instant);
                                    this.Instants.Add(instant);
                                }
                                break;

                            default:
                                throw new IOException("Unknown marker 0x" + marker.ToString("X8"));
                        }
                    }
                    catch (EndOfStreamException)
                    {
                        break;
                    }
                }
            }
        }
    }
}
