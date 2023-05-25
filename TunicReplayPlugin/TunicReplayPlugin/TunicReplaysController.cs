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
        private const float GhostScale = 0.75f;

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

            var lureGameObject = lurePrefab.gameObject;
            
            // Deactivate the lure prefab so that the instantiated ghost is initially deactivated
            var lureActive = lureGameObject.activeSelf;
            lureGameObject.SetActive(false);

            var ghostGameObject = Instantiate(lureGameObject);
            ghostGameObject.name = $"Replay Ghost #{player}";
            ghostGameObject.transform.localScale = new Vector3(GhostScale, GhostScale, GhostScale);

            lureGameObject.SetActive(lureActive);

            foreach (var child in ghostGameObject.GetComponents<Component>())
            {
                var childType = child.GetIl2CppType();
                if (childType != UnhollowerRuntimeLib.Il2CppType.Of<Animator>() &&
                    childType != UnhollowerRuntimeLib.Il2CppType.Of<Transform>())
                {
                    Logger.LogInfo("Destroying " + childType.Name + " on ghost");
                    Destroy(child);
                }
            }

            // TODO: Randomize player colors

            ghostGameObject.SetActive(true);

            // Destroy FMOD behaviors so that it doesn't play the running sound
            // This can only be done when the game object is active, otherwise GetBehaviours doesn't return any behaviors
            foreach (var behavior in ghostGameObject.GetComponent<Animator>().GetBehaviours<StateMachineBehaviour>())
            {
                if (behavior.GetIl2CppType() == UnhollowerRuntimeLib.Il2CppType.Of<FMODAnimationStateBehaviour>())
                {
                    Logger.LogInfo("Destroying " + behavior.GetIl2CppType().Name + " on ghost animator");
                    Destroy(behavior);
                }
            }

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
