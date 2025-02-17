using System;
using UnityEngine;

public enum State
{
   None,
   White,
   Black
}

public static class AppState
{
   public static State GetCurrentState()
   {
      return PlayerPrefs.GetString(Constants.CurrentAppState, "None").ToEnum<State>();
   }
   
   public static State GetConstantState()
   {
      return PlayerPrefs.GetString(Constants.ConstantAppState, "None").ToEnum<State>();
   }
   
   public static void SetConstantBlackState()
   {
      SetKey(Constants.ConstantAppState, State.Black.ToString());
      SetKey(Constants.CurrentAppState, State.Black.ToString());
   }
   
   public static void SetConstantWhiteState()
   {
      SetKey(Constants.ConstantAppState, State.White.ToString());
      SetKey(Constants.CurrentAppState, State.White.ToString());
   }
   
   public static void SetCurrentBlackState()
   {
      SetKey(Constants.CurrentAppState, State.Black.ToString());
   }
   
   public static void SetCurrentWhiteState()
   {
      SetKey(Constants.CurrentAppState, State.White.ToString());
   }
   
   public static bool GetAppStartedSuccess()
   {
      return PlayerPrefs.HasKey(Constants.IsAppStartedSuccess);
   }

   public static void SetAppStartedSuccess()
   {
      SetKey(Constants.IsAppStartedSuccess, "Success");
   }

   public static void Clear()
   {
      PlayerPrefs.DeleteKey(Constants.CurrentAppState);
      
      PlayerPrefs.Save();
   }

   private static void SetKey(string key, string value)
   {
      PlayerPrefs.SetString(key, value);
      
      PlayerPrefs.Save();
   }
   
   private static T ToEnum<T>(this string value)
   {
      return (T) Enum.Parse(typeof(T), value, true);
   }
}
