using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Views.General;
using Names;

namespace Controllers.Scenes
{
    public class SettingsSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private GameObject _notificationPanel;
        [SerializeField] 
        private PanelView _privacyPanel;
        [SerializeField] 
        private PanelView _termsPanel;
        
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _backBtn;
        [SerializeField] 
        private Button _ppBtn;
        [SerializeField] 
        private Button _termsBtn;
        [SerializeField] 
        private Button _clearDataBtn;
        
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
            
        }

        protected override void Subscribe()
        {
            _backBtn.onClick.AddListener(OnPressBackBtn);
            _clearDataBtn.onClick.AddListener(OnPressClearDataBtn);
            _ppBtn.onClick.AddListener(OnPressPrivacyBtn);
            _termsBtn.onClick.AddListener(OnPressTermsBtn);
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
            _clearDataBtn.onClick.RemoveAllListeners();
            _ppBtn.onClick.RemoveAllListeners();
            _termsBtn.onClick.RemoveAllListeners();
        }

        private void OnPressBackBtn()
        {
            OpenMenu();
        }

        private void OnPressClearDataBtn()
        {
            ClearData();
        }

        private void OnPressPrivacyBtn()
        {
            base.SetClickClip();
            
            OpenPrivacyPanel();
        }

        private void OnPressTermsBtn()
        {
            base.SetClickClip();
            
            OpenTermsPanel();
        }

        private void OpenPrivacyPanel()
        {
            _privacyPanel.PressBtnAction += OnReceiveAnswerPrivacyPanel;
            _privacyPanel.gameObject.SetActive(true);
        }
        
        private void OpenTermsPanel()
        {
            _termsPanel.PressBtnAction += OnReceiveAnswerTermsPanel;
            _termsPanel.gameObject.SetActive(true);
        }

        private void OnReceiveAnswerPrivacyPanel(int answer)
        {
            base.SetClickClip();
            
            _privacyPanel.PressBtnAction -= OnReceiveAnswerPrivacyPanel;
            _privacyPanel.gameObject.SetActive(false);
        }

        private void OnReceiveAnswerTermsPanel(int answer)
        {
            base.SetClickClip();
            
            _termsPanel.PressBtnAction -= OnReceiveAnswerTermsPanel;
            _termsPanel.gameObject.SetActive(false);
        }

        private void OpenMenu()
        {
            base.LoadScene(SceneName.Menu);
        }

        private void ClearData()
        {
            base.SetClickClip();
            
            PlayerPrefs.DeleteAll();

            StartCoroutine(DelayShowNotificationPanel());
        }

        private IEnumerator DelayShowNotificationPanel()
        {
            _notificationPanel.SetActive(true);

            yield return new WaitForSeconds(2);
            
            _notificationPanel.SetActive(false);
        }
    }
}