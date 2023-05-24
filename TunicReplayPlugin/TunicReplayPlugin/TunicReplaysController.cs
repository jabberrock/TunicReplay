using System;
using System.Collections.Generic;
using System.Linq;
using UnhollowerBaseLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TunicReplayPlugin
{
    internal class TunicReplaysController : MonoBehaviour
    {
        private const string TunicReplaysDir = @"%USERPROFILE%\Desktop\TunicReplays";
        private const string TitleScreenSceneName = "TitleScreen";
        private const string LoadingSceneName = "Loading";
        private const string SaveSceneIndexesKey = "Tunic Replay Scene Indexes";
        private const string SavePlayTimeKey = "playtime";

        private ReplayDatabase replayDatabase;
        private string lastSceneName = String.Empty;

        private Dictionary<int, GameObject> playerGameObjects = new Dictionary<int, GameObject>();

        public void Start()
        {
            this.replayDatabase =
                new ReplayDatabase(Environment.ExpandEnvironmentVariables(TunicReplaysDir));
        }

        public void Update()
        {
            var scene = SceneManager.GetActiveScene();
            if (!scene.IsValid())
            {
                return;
            }

            if (this.lastSceneName != scene.name)
            {
                Logger.LogInfo($"Switching scenes from {this.lastSceneName} -> {scene.name}".ToString());
                this.playerGameObjects.Clear();

                // TODO: Do we skip if we're returning to the title screen from in game?
                // TODO: Don't reload replays that are already loaded
                // TODO: Listen for new replays and try to load them
                if (scene.name == TitleScreenSceneName)
                {
                    this.replayDatabase.LoadReplays();
                }
                
                if (this.lastSceneName == LoadingSceneName && scene.name != TitleScreenSceneName)
                {
                    var sceneIndexes = new List<int>();

                    var sceneIndexesStr = SaveFile.GetString(SaveSceneIndexesKey);
                    if (!string.IsNullOrEmpty(sceneIndexesStr))
                    {
                        foreach (var part in sceneIndexesStr.Split(','))
                        {
                            sceneIndexes.Add(int.Parse(part));
                        }
                    }

                    sceneIndexes.Add(scene.buildIndex);
                    SaveFile.SetString(SaveSceneIndexesKey, string.Join(",", sceneIndexes));

                    this.replayDatabase.JumpToSegment(sceneIndexes, SaveFile.GetFloat(SavePlayTimeKey));
                }
            }

            this.lastSceneName = scene.name;

            if (SpeedrunData.timerRunning)
            {
                var replayInstants = this.replayDatabase.At(SpeedrunData.inGameTime);
                foreach (var replayInstant in replayInstants)
                {
                    GameObject playerGameObject;
                    if (!this.playerGameObjects.TryGetValue(replayInstant.Player, out playerGameObject))
                    {
                        playerGameObject = CreatePlayer(replayInstant.Player);
                        this.playerGameObjects[replayInstant.Player] = playerGameObject;
                    }

                    playerGameObject.transform.SetPositionAndRotation(replayInstant.Position, replayInstant.Rotation);
                }
            }
        }

        private GameObject CreatePlayer(int player)
        {
            Logger.LogInfo("Creating ghost for player " + player);
            
            var lurePrefab = Resources.FindObjectsOfTypeAll<Bait>().FirstOrDefault();
            if (lurePrefab == null)
            {
                Logger.LogWarning("Failed to find lure prefab");
                return null;
            }

            var ghostGameObject = new GameObject($"Replay Ghost #{player}");

            var skeletonGameObject = Instantiate(lurePrefab.transform.Find("Fox"), ghostGameObject.transform);
            skeletonGameObject.name = "Fox";

            var meshGameObject = Instantiate(lurePrefab.transform.Find("fox"), ghostGameObject.transform);
            meshGameObject.name = "fox";

            var renderer = meshGameObject.GetComponent<SkinnedMeshRenderer>();
            renderer.rootBone = ghostGameObject.transform.Find(GetRelativePath(renderer.rootBone, lurePrefab.transform));

            var newBones = new Il2CppReferenceArray<Transform>(renderer.bones.Count);
            for (int i = 0; i < renderer.bones.Count; ++i)
            {
                if (renderer.bones[i] != null)
                {
                    newBones[i] = ghostGameObject.transform.Find(GetRelativePath(renderer.bones[i], lurePrefab.transform));
                }
            }
            renderer.bones = newBones;

            // TODO: Randomize player colors

            return ghostGameObject;
        }

        private static string GetRelativePath(Transform transform, Transform rootTransform)
        {
            if (transform.parent != null && transform.parent != rootTransform)
            {
                return GetRelativePath(transform.parent, rootTransform) + "/" + transform.name;
            }
            else
            {
                return transform.name;
            }
        }
    }
}
