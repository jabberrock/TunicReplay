using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace TunicReplayPlugin
{
    internal class ReplaySettings : MonoBehaviour
    {
        private const string TitleScreenSceneName = "TitleScreen";

        public void OnGUI()
        {
            var scene = SceneManager.GetActiveScene();
            if (!scene.IsValid() || scene.name != TitleScreenSceneName)
            {
                return;
            }

            Cursor.visible = true;
            GUI.Window(101, new Rect(20f, 150f, 400f, 300f), new Action<int>(ReplaySettingsWindow), "Replay Settings");
        }

        private static void ReplaySettingsWindow(int windowID)
        {
            var replayDatabase = ReplayDatabase.Instance;
            if (replayDatabase == null)
            {
                return;
            }

            GUI.skin.label.fontSize = 20;
            GUI.skin.toggle.fontSize = 20;

            GUI.Label(new Rect(10f, 20f, 300f, 30f), "Race Mode");
            bool timeInGame = GUI.Toggle(new Rect(10f, 60f, 300f, 30f), replayDatabase.Mode == ReplayDatabase.ReplayMode.TimeInGame, "Game Time");
            bool timeInSegment = GUI.Toggle(new Rect(10f, 100f, 300f, 30f), replayDatabase.Mode == ReplayDatabase.ReplayMode.TimeInSegment, "Segment Time");
            bool bestTimeInSegment = GUI.Toggle(new Rect(10f, 140f, 300f, 30f), replayDatabase.Mode == ReplayDatabase.ReplayMode.BestTimeInSegment, "Best Segment Time");

            if (timeInGame && replayDatabase.Mode != ReplayDatabase.ReplayMode.TimeInGame)
            {
                Logger.LogInfo("Switching replay mode to time in game");
                replayDatabase.Mode = ReplayDatabase.ReplayMode.TimeInGame;
            }
            else if (timeInSegment && replayDatabase.Mode != ReplayDatabase.ReplayMode.TimeInSegment)
            {
                Logger.LogInfo("Switching replay mode to time in segment");
                replayDatabase.Mode = ReplayDatabase.ReplayMode.TimeInSegment;
            }
            else if (bestTimeInSegment && replayDatabase.Mode != ReplayDatabase.ReplayMode.BestTimeInSegment)
            {
                Logger.LogInfo("Switching replay mode to best time in segment");
                replayDatabase.Mode = ReplayDatabase.ReplayMode.BestTimeInSegment;
            }
        }
    }
}
