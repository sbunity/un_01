using UnityEngine;
using UnityEngine.UI;

using Views.Menu;
using Names;

namespace Controllers.Scenes
{
    public class MenuSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private GameBtnView _classicGameBtn;
        [SerializeField] 
        private GameBtnView _collectionGameBtn;
        [SerializeField] 
        private GameBtnView _collectGameBtn;
        [SerializeField] 
        private Button _settingsbtn;
        
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
            _classicGameBtn.PressBtnAction += OnStartAnimBtn;
            _collectionGameBtn.PressBtnAction += OnStartAnimBtn;
            _collectGameBtn.PressBtnAction += OnStartAnimBtn;
            
            _classicGameBtn.EndAnimAction += OnPressClassicGameBtn;
            _collectionGameBtn.EndAnimAction += OnPressCollectionGameBtn;
            _collectGameBtn.EndAnimAction += OnPressCollectGameBtn;
            
            _settingsbtn.onClick.AddListener(OnPressSettingsBtn);
        }

        protected override void Unsubscribe()
        {
            _classicGameBtn.PressBtnAction -= OnStartAnimBtn;
            _collectionGameBtn.PressBtnAction -= OnStartAnimBtn;
            _collectGameBtn.PressBtnAction -= OnStartAnimBtn;
            
            _classicGameBtn.EndAnimAction -= OnPressClassicGameBtn;
            _collectionGameBtn.EndAnimAction -= OnPressCollectionGameBtn;
            _collectGameBtn.EndAnimAction -= OnPressCollectGameBtn;
            
            _settingsbtn.onClick.RemoveAllListeners();
        }

        private void OnStartAnimBtn()
        {
            _classicGameBtn.DeactivateBtn();
            _collectionGameBtn.DeactivateBtn();
            _collectGameBtn.DeactivateBtn();
            _settingsbtn.interactable = false;
        }

        private void OnPressClassicGameBtn()
        {
            OpenClassicGame();
        }

        private void OnPressCollectionGameBtn()
        {
            OpenCollectionGame();
        }

        private void OnPressCollectGameBtn()
        {
            OpenCollectGame();
        }

        private void OnPressSettingsBtn()
        {
            OnStartAnimBtn();
            
            OpenSettings();
        }

        private void OpenClassicGame()
        {
            base.LoadScene(SceneName.ClassicGame);
        }

        private void OpenCollectionGame()
        {
            base.LoadScene(SceneName.CollectionGame);
        }

        private void OpenCollectGame()
        {
            base.LoadScene(SceneName.CollectGame);
        }

        private void OpenSettings()
        {
            base.LoadScene(SceneName.Settings);
        }
    }
}