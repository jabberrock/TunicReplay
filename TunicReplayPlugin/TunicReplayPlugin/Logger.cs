using BepInEx.Logging;

namespace TunicReplayPlugin
{
    internal class Logger
    {
        private static ManualLogSource Log = null;

        public static void SetLogger(ManualLogSource log)
        {
            Log = log;
        }

        public static void LogDebug(object data)
        {
            if (Log != null)
            {
                Log.LogDebug(data);
            }
        }

        public static void LogInfo(object data)
        {
            if (Log != null)
            {
                Log.LogInfo(data);
            }
        }

        public static void LogWarning(object data)
        {
            if (Log != null)
            {
                Log.LogWarning(data);
            }
        }

        public static void LogError(object data)
        {
            if (Log != null)
            {
                Log.LogError(data);
            }
        }

        public static void LogFatal(object data)
        {
            if (Log != null)
            {
                Log.LogFatal(data);
            }
        }
    }
}
