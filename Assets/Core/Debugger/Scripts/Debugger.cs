using UnityEngine;

namespace Core
{
    public static class Debugger
    {
        private static bool _isLogging;
        
        public static void Log(object message, Color color = default)
        {
            if (!Settings.IsDebug()) return;
            
            if (color == default)
            {
                color = Color.gray;
            }   
            
            string colorCode = ColorUtility.ToHtmlStringRGBA(color);
            Debug.Log($"<color=#{colorCode}>{message}</color>");
        }
        
        public static void LogError(object message, Color color = default)
        {
            if (!Settings.IsDebug()) return;
            
            if (color == default)
            {
                color = Color.gray;
            } 
            
            string colorCode = ColorUtility.ToHtmlStringRGBA(color);
            Debug.LogError($"<color=#{colorCode}>{message}</color>");
        }
        
        public static void LogWarning(object message, Color color = default)
        {
            if (!Settings.IsDebug()) return;
            
            if (color == default)
            {
                color = Color.gray;
            } 
            
            string colorCode = ColorUtility.ToHtmlStringRGBA(color);
            Debug.LogWarning($"<color=#{colorCode}>{message}</color>");
        }
    }
}

