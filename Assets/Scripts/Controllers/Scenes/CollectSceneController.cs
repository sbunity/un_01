using UnityEngine;
using UnityEngine.UI;

using Controllers.CollectGame;
using Views.ClassicGame;
using Views.CollectGame;

using Models;
using Names;
using Enums;


namespace Controllers.Scenes
{
    public class CollectSceneController : AbstractSceneController
    {
        [Space(5)] [Header("Controller")]
        [SerializeField] 
        private SpawnController _spawnController;
        
        [Space(5)] [Header("Views")] 
        [SerializeField]
        private BetView _betView;
        [SerializeField] 
        private PrizeUpdaterView _prizeUpdaterView;
        [SerializeField] 
        private GameObject _mainPanel;
        [SerializeField] 
        private WinPanel _winPanel;
        [SerializeField]
        private LosePanel _losePanel;

        [Space(5)] [Header("Buttons")] 
        [SerializeField]
        private Button _startBtn;
        [SerializeField] 
        private Button _stopBtn;
        [SerializeField] 
        private Button _backBtn;
        
        private CollectGameModel _model;
        
        protected override void OnSceneEnable()
        {
            UpdateBet();
            UpdatePrize();
            SetStateStartStopBtns(true);
        }

        protected override void OnSceneStart()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        protected override void Initialize()
        {
            _model = new CollectGameModel();
            
            _spawnController.Initialize();
        }

        protected override void Subscribe()
        {
            _startBtn.onClick.AddListener(OnPressStartBtn);
            _stopBtn.onClick.AddListener(OnPressStopBtn);
            _backBtn.onClick.AddListener(OnPressBackBtn);
            
            _betView.PressBtnAction += OnPressBetBtn;
            _spawnController.PressBaloonAction += OnPressBalloon;
        }

        protected override void Unsubscribe()
        {
            _startBtn.onClick.RemoveAllListeners();
            _stopBtn.onClick.RemoveAllListeners();
            _backBtn.onClick.RemoveAllListeners();
            
            _betView.PressBtnAction -= OnPressBetBtn;
            _spawnController.PressBaloonAction -= OnPressBalloon;
            
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
            _spawnController.EndGame();
            OpenMenu();
        }

        private void OnPressStartBtn()
        {
            base.SetClickClip();
            
            _model.PurchaseBet();
            _betView.HideButtons();
            SetStateStartStopBtns(false);
            _spawnController.StartGame();
        }

        private void OnPressStopBtn()
        {
            base.SetClickClip();
            
            _spawnController.EndGame();
            
            OnEndGame();
        }

        private void OnPressBalloon(int prize)
        {
            base.PlaySound(AudioNames.Destroy);
            
            _model.AddPrize(prize);
            
            UpdatePrize();
        }

        private void UpdateBet()
        {
            _betView.UpdateCount(_model.Bet);
        }
        
        private void SetActiveBetBtns()
        {
            _betView.SetActiveMinusBtn(_model.IsActiveMinusBtn);
            _betView.SetActivePlusBtn(_model.IsActivePlusBtn);
        }

        private void SetStateStartStopBtns(bool value)
        {
            _startBtn.gameObject.SetActive(value);
            _stopBtn.gameObject.SetActive(!value);

            if (value)
            {
                _startBtn.interactable = _model.CanStartGame;
            }
        }

        private void OnEndGame()
        {
            if (_model.IsWin)
            {
                base.PlaySound(AudioNames.WinClip);
                
                OpenWinPanel();
            }
            else
            {
                base.PlaySound(AudioNames.LoseClip);
                
                OpenLosePanel();
            }
        }

        private void OpenWinPanel()
        {
            _winPanel.UpdateDescription(_model.Reward);
            _winPanel.PressBtnAction += OnReceiveAnswerWinPanel;
            
            _mainPanel.SetActive(false);
            _winPanel.gameObject.SetActive(true);
        }

        private void OpenLosePanel()
        {
            _losePanel.PressBtnAction += OnReceiveAnswerLosePanel;
            _mainPanel.SetActive(false);
            _losePanel.gameObject.SetActive(true);
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

        private void UpdatePrize()
        {
            _prizeUpdaterView.UpdateText(_model.Prize);
        }

        private void OpenMenu()
        {
            base.LoadScene(SceneName.Menu);
        }

        private void RestartGame()
        {
            base.LoadScene(SceneName.CollectGame);
        }
    }
}