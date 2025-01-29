using System.Collections;
using UnityEngine;
using UnityEngine.UI;

using Controllers.CollectionGame;
using Enums;
using Views.CollectionGame;
using Models;
using Names;

namespace Controllers.Scenes
{
    public class CollectionGameSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private SpawnBaloonController _spawnBaloonController;
        
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private CollectionPanel _collectionPanel;
        [SerializeField] 
        private GameObject _rewardPanel;
        [SerializeField] 
        private GameObject _mainPanel;
        [SerializeField] 
        private ResultPanel _resultPanel;
        [SerializeField] 
        private ProgressView _progressView;

        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _backBtn;
        
        private CollectionGameModel _model;
        
        protected override void OnSceneEnable()
        {
            OpenFirstPanel();
        }

        protected override void OnSceneStart()
        {
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new CollectionGameModel();
            
            _spawnBaloonController.Initialize(_model.ProgressIndexes);
        }

        protected override void Subscribe()
        {
            _backBtn.onClick.AddListener(OnPressBackBtn);
            
            _spawnBaloonController.PressBaloonAction += OnPressBaloon;
            _spawnBaloonController.AllBaloonsFinishedMoveAction += OnEndGame;
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
            
            _spawnBaloonController.PressBaloonAction -= OnPressBaloon;
            _spawnBaloonController.AllBaloonsFinishedMoveAction -= OnEndGame;
        }

        private void OpenFirstPanel()
        {
            if (_model.IsRestart)
            {
                OpenMainPanel();
            }
            else
            {
                OpenCollectionPanel();
            }
        }

        private void OpenCollectionPanel()
        {
            UpdateCollectPanelStates();
            _collectionPanel.gameObject.SetActive(true);

            _collectionPanel.PressCollectBtnAction += OnPressCollectBtn;
            _collectionPanel.PressBtnAction += OnReceiveAnswerCollectionPanel;
        }

        private void ShowRewardAnim()
        {
            base.PlaySound(AudioNames.WinClip);
            
            _model.AddRewardToBalance();
            StartCoroutine(DelayShowRewardPanel());
        }

        private void OnPressCollectBtn(int index)
        {
            base.SetClickClip();
            
            _model.ResetItemCount(index);
            UpdateCollectPanelStates();
            
            ShowRewardAnim();
        }

        private void OnPressBaloon(int index)
        {
            base.PlaySound(AudioNames.Destroy);
            
            _model.ChangeProgressState(index);
            
            UpdateProgress();
        }

        private void OnPressBackBtn()
        {
            _model.IsRestart = false;
            
            base.LoadScene(SceneName.CollectionGame);
        }

        private void OnEndGame()
        {
            _mainPanel.gameObject.SetActive(false);
            OpenResultPanel();
        }

        private void OnReceiveAnswerCollectionPanel(int answer)
        {
            _collectionPanel.PressCollectBtnAction -= OnPressCollectBtn;
            _collectionPanel.PressBtnAction -= OnReceiveAnswerCollectionPanel;

            switch (answer)
            {
                case 0:
                    OpenMenu();
                    break;
                case 1:
                    base.SetClickClip();
                    _collectionPanel.gameObject.SetActive(false);
                    OpenMainPanel();
                    break;
            }
        }
        
        private void OnReceiveAnswerResultPanel(int answer)
        {
            _resultPanel.PressBtnAction -= OnReceiveAnswerResultPanel;

            _model.IsRestart = answer == 1;

            base.LoadScene(SceneName.CollectionGame);
        }

        private void UpdateCollectPanelStates()
        {
            _collectionPanel.SetCounts(_model.GetCollectionCounts());
            _collectionPanel.SetFills(_model.GetCollectionFillAmounts());
            _collectionPanel.SetStates(_model.GetCollectionStates());
        }

        private void OpenMainPanel()
        {
            _mainPanel.SetActive(true);
            
            UpdateProgress();
            
            _spawnBaloonController.StartGame();
        }

        private void OpenResultPanel()
        {
            base.PlaySound(AudioNames.WinClip);
            
            _resultPanel.Initialize(_model.ProgressIndexes, _model.ProgressStates);
            _mainPanel.gameObject.SetActive(false);
            _resultPanel.gameObject.SetActive(true);
            _resultPanel.PressBtnAction += OnReceiveAnswerResultPanel;
        }

        private void UpdateProgress()
        {
            _progressView.UpdateProgressIcons(_model.ProgressIndexes, _model.ProgressStates);
        }

        private void OpenMenu()
        {
            _model.IsRestart = false;
            base.LoadScene(SceneName.Menu);
        }

        private IEnumerator DelayShowRewardPanel()
        {
            _rewardPanel.SetActive(true);

            yield return new WaitForSeconds(3);
            
            _rewardPanel.gameObject.SetActive(false);
        }
    }
}