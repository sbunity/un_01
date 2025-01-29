using System;
using UnityEngine;
using UnityEngine.SceneManagement;

using Models;
using Names;

namespace Controllers.Scenes
{
    public class InitSceneController : MonoBehaviour
    {
        private OnboardingModel _model;

        private void Awake()
        {
            _model = new OnboardingModel();
        }

        private void OnEnable()
        {
            SceneName sceneName = _model.CanOpenScene ? SceneName.Onboarding : SceneName.Menu;
            
            LoadScene(sceneName);
        }

        private void LoadScene(SceneName sceneName)
        {
            SceneManager.LoadScene(sceneName.ToString());
        }
    }
}