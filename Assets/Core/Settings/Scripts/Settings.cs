using Core.AppInstallTracker;
using UnityEngine;

namespace Core
{
    [CreateAssetMenu(fileName = "Settings", menuName = "ScriptableObjects/Settings", order = 1)]
    public class Settings : ScriptableObject
    {
        [SerializeField] private string firebaseRemoteKey = "mrgame_string";

        [SerializeField] private string landingUrl = "https://google.com";

        //[SerializeField] private string additionalUrl = "";
        
        [SerializeField] private string oneSignalAppID;
        
        [SerializeField] private string privacyPoliceUrl;
        
        [SerializeField] private string termsOfUseUrl;
        
        [SerializeField] private bool isDebug;
        
        [SerializeField] private DateTimeWrapper birthDate;

        private static Settings _instance;

        private static Settings Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = Resources.Load<Settings>("Settings");
                }

                if (_instance == null)
                {
                    Debug.LogError("Settings asset not found in Resources!");
                }
                
                return _instance;
            }
        }

        public static string FirebaseKey()
        {
            return Instance == null ? null : Instance.firebaseRemoteKey;
        }

        public static bool IsDebug()
        {
            return Instance != null && Instance.isDebug;
        }
        
        public static string DefaultB1n0m()
        {
            return Instance == null ? null : Instance.landingUrl;
        }
        
        /*public static string AdditionalUrl()
        {
            return Instance == null ? null : Instance.additionalUrl;
        }*/
        
        public static string OneSignalAppID()
        {
            return Instance == null ? null : Instance.oneSignalAppID;
        }
        
        public static string PolicyOrTermsUrl(PolicyOrTerms value)
        {
            if (Instance == null) return null;
            
            return value == PolicyOrTerms.PrivacyPolice? Instance.privacyPoliceUrl : Instance.termsOfUseUrl;
        }
        
        public static DateTimeWrapper BirthDate()
        {
            return Instance == null ? null : Instance.birthDate;
        }
    }

    [System.Serializable]
    public enum PolicyOrTerms
    {
        PrivacyPolice,
        TermsOfUse
    }
}
