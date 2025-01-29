using UnityEngine;
using UnityEngine.UI;

using Models;
using Names;

namespace Controllers.Scenes
{
    public class OnBoardingSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _startBtn;
        
        private OnboardingModel _model;
        
        protected override void OnSceneEnable()
        {
            
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new OnboardingModel();
        }

        protected override void Subscribe()
        {
            _startBtn.onClick.AddListener(OnPressStartBtn);
        }

        protected override void Unsubscribe()
        {
            _startBtn.onClick.RemoveAllListeners();
        }

        private void OnPressStartBtn()
        {
            _model.SetDisableState();
            
            OpenMenuScene();
        }

        private void OpenMenuScene()
        {
            base.LoadScene(SceneName.Menu);
        }
    }
}