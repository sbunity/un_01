using UnityEngine;

namespace Models
{
    public class OnboardingModel
    {
        private const string SceneStateKey = "OnboardingModel.SceneState";

        public bool CanOpenScene => !PlayerPrefs.HasKey(SceneStateKey);

        public void SetDisableState()
        {
            PlayerPrefs.SetInt(SceneStateKey, 0);
        }
    }
}