using System.Diagnostics;
using Debug = UnityEngine.Debug;

namespace Main
{
    public static class Logger
    {
        [Conditional("DEBUG")]
        public static void Log(string message)
        {
            Debug.Log(message);
        }

        public static void Warning(string message)
        {
            Debug.LogWarning(message);
        }

        public static void Error(string message)
        {
            Debug.LogError(message);
        }
    }
}