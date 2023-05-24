using BepInEx;
using BepInEx.IL2CPP;
using UnhollowerRuntimeLib;
using UnityEngine;

namespace TunicReplayPlugin
{
    [BepInPlugin(PluginInfo.Guid, PluginInfo.Name, PluginInfo.Version)]
    public class TunicReplays : BasePlugin
    {

        public override void Load()
        {
            Logger.SetLogger(this.Log);

            Logger.LogInfo("Loading plugin...");

            Application.runInBackground = true;

            ClassInjector.RegisterTypeInIl2Cpp<TunicReplaysController>();

            var tunicReplays = new GameObject("Tunic Replay") { hideFlags = HideFlags.HideAndDontSave };
            tunicReplays.AddComponent<TunicReplaysController>();
            GameObject.DontDestroyOnLoad(tunicReplays);

            Logger.LogInfo("Plugin loaded");
        }
    }
}
