using System.Collections.Generic;
using Controllers.ClassicGame;

using UnityEngine;
using UnityEngine.UI;

using Views.ClassicGame;
using Models;
using Names;
using Enums;

namespace Controllers.Scenes
{
    public class ClassicGameSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Controller")] 
        [SerializeField]
        private StartAnimBalloonController _animBalloonController;
            
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private BetView _betView;
        [SerializeField] 
        private CoefficientUpdaterView _coefficientUpdaterView;
        [SerializeField] 
        private GameObject _description;
        [SerializeField] 
        private List<BaloonScallerView> _baloonScallerViews;
        [SerializeField] 
        private GameObject _mainPanel;
        [SerializeField] 
        private WinPanel _winPanel;
        [SerializeField] 
        private LosePanel _losePanel;

        [Space(5)] [Header("Buttons")]
        [SerializeField]
        private Button _backBtn;
        [SerializeField] 
        private Button _stopBtn;
        
        private ClassicGameModel _model;
        
        protected override void OnSceneEnable()
        {
            UpdateBet();
            SetActiveBetBtns();
            UpdateCoefficient();
        }

        protected override void OnSceneStart()
        {
            StartAnim();
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new ClassicGameModel(_baloonScallerViews.Count);
            
            SetScaleBaloons();
        }

        protected override void Subscribe()
        {
            _backBtn.onClick.AddListener(OnPressBackBtn);
            _stopBtn.onClick.AddListener(OnPressStopBtn);
            
            _betView.PressBtnAction += OnPressBetBtn;
            _baloonScallerViews.ForEach(x => x.StartPressAction += OnBaloonPress);
            _baloonScallerViews.ForEach(x => x.PausePressAction += OnCanUpdateCoefficient);
            _baloonScallerViews.ForEach(x => x.EndPressAction += OnCanResetCoefficient);
            _baloonScallerViews.ForEach(x => x.StartExplodeAnimAction += PlaySoundDestroy);
        }

        protected override void Unsubscribe()
        {
            _backBtn.onClick.RemoveAllListeners();
            _stopBtn.onClick.RemoveAllListeners();
            
            _betView.PressBtnAction -= OnPressBetBtn;
            _baloonScallerViews.ForEach(x => x.StartPressAction -= OnBaloonPress);
            _baloonScallerViews.ForEach(x => x.PausePressAction -= OnCanUpdateCoefficient);
            _baloonScallerViews.ForEach(x => x.EndPressAction -= OnCanResetCoefficient);
            _baloonScallerViews.ForEach(x => x.StartExplodeAnimAction -= PlaySoundDestroy);
        }

        private void SetScaleBaloons()
        {
            for (int i = 0; i < _baloonScallerViews.Count; i++)
            {
                _baloonScallerViews[i].SetTargetScale(_model.GetScaleByIndex(i));
            }
        }

        private void OnPressBetBtn(bool value)
        {
            base.SetClickClip();
            
            _model.ChangeBet(value);
            
            UpdateBet();
            SetActiveBetBtns();
        }

        private void OnPressBackBtn()
        {
            OpenMenu();
        }

        private void OnBaloonPress()
        {
            _model.PurchaseBet();
            _baloonScallerViews.ForEach(x => x.StartPressAction -= OnBaloonPress);
            _description.SetActive(false);
            _betView.HideButtons();
            ActivateStopBtn();
        }

        private void OnPressStopBtn()
        {
            base.SetClickClip();
            
            OnEndGame(_model.IsWin);
        }

        private void OnCanUpdateCoefficient(BaloonScallerView baloonScallerView, float coeff)
        {
            int index = _baloonScallerViews.IndexOf(baloonScallerView);
            
            _model.UpdateCoefficientBuIndex(index, coeff);
            
            UpdateCoefficient();
        }

        private void OnCanResetCoefficient(BaloonScallerView baloonScallerView)
        {
            int index = _baloonScallerViews.IndexOf(baloonScallerView);
            
            _model.ResetCoefficientByIndex(index);
            
            UpdateCoefficient();

            if (!_model.IsWin)
            {
                OnEndGame(false);
            }
        }

        private void UpdateBet()
        {
            _betView.UpdateCount(_model.Bet);
        }

        private void PlaySoundDestroy()
        {
            if (_model.IsLastBalloon)
            {
                _stopBtn.interactable = false;
            }

            base.PlaySound(AudioNames.Destroy);
        }

        private void SetActiveBetBtns()
        {
            _betView.SetActiveMinusBtn(_model.IsActiveMinusBtn);
            _betView.SetActivePlusBtn(_model.IsActivePlusBtn);
        }

        private void UpdateCoefficient()
        {
            _coefficientUpdaterView.UpdateText(_model.GetCoefficient());
        }

        private void ActivateStopBtn()
        {
            _stopBtn.interactable = true;
        }

        private void OnEndGame(bool value)
        {
            if (value)
            {
                base.PlaySound(AudioNames.WinClip);
                
                OpenWinPanel();
            }
            else
            {
                base.PlaySound(AudioNames.LoseClip);
                
                OpenLosePanel();
            }
            
            _mainPanel.SetActive(false);
        }

        private void OpenWinPanel()
        {
            _model.AddRewardToBalance();
            _winPanel.UpdateDescription(_model.GetReward());
            _winPanel.gameObject.SetActive(true);
            _winPanel.PressBtnAction += OnReceiveAnswerWinPanel;
        }

        private void OpenLosePanel()
        {
            _losePanel.gameObject.SetActive(true);
            _losePanel.PressBtnAction += OnReceiveAnswerLosePanel;
        }

        private void OpenMenu()
        {
            base.LoadScene(SceneName.Menu);
        }

        private void RestartGame()
        {
            base.LoadScene(SceneName.ClassicGame);
        }

        private void OnReceiveAnswerWinPanel(int answer)
        {
            _winPanel.PressBtnAction -= OnReceiveAnswerWinPanel;

            switch (answer)
            {
                case 0:
                    OpenMenu();
                    break;
                case 1:
                    RestartGame();
                    break;
            }
        }
        
        private void OnReceiveAnswerLosePanel(int answer)
        {
            _losePanel.PressBtnAction -= OnReceiveAnswerLosePanel;

            switch (answer)
            {
                case 0:
                    OpenMenu();
                    break;
                case 1:
                    RestartGame();
                    break;
            }
        }

        private void StartAnim()
        {
            _animBalloonController.StartAnim();
            _animBalloonController.AnimationEndedAction += OnStartAnimCompleted;
        }

        private void OnStartAnimCompleted()
        {
            _animBalloonController.AnimationEndedAction -= OnStartAnimCompleted;
            
            _description.SetActive(true);
            _baloonScallerViews.ForEach(x => x.gameObject.SetActive(true));
        }
    }
}