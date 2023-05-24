using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace TunicReplayPlugin
{
    internal class Replay
    {
        private readonly List<ReplaySegment> segments;

        public Replay(int player, string replayPath)
        {
            this.segments = LoadReplaySegments(player, replayPath);

            Logger.LogInfo("Loaded replay at " + replayPath + " with " + this.segments.Count + " segments");
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
                if (nextSegmentIndex < this.segments.Count
                    && this.segments[nextSegmentIndex].SceneIndex == sceneIndex)
                {
                    segmentIndex = nextSegmentIndex;
                }
            }

            // Make sure the segment matches the final requested scene
            if (segmentIndex >= 0 && this.segments[segmentIndex].SceneIndex == sceneIndexes[sceneIndexes.Count - 1])
            {
                return this.segments[segmentIndex];
            }

            return null;
        }

        private static List<ReplaySegment> LoadReplaySegments(int player, string replayPath)
        {
            var segments = new List<ReplaySegment>();

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
                                var sceneIndex = binaryReader.ReadInt32();
                                var startGameTime = binaryReader.ReadSingle();

                                var segment = new ReplaySegment()
                                {
                                    Player = player,
                                    SceneIndex = sceneIndex,
                                    StartReplayGameTime = startGameTime
                                };
                                segments.Add(segment);

                                break;

                            case 0xCAFED00D:
                                if (segments.Count > 0)
                                {
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
                                        ReplayGameTime = gameTime,
                                        Position = new Vector3(px, py, pz),
                                        Rotation = new Quaternion(rx, ry, rz, rw)
                                    };
                                    segments[segments.Count - 1].Instants.Add(instant);
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

            return segments;
        }
    }
}
