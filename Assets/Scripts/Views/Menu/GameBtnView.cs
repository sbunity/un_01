using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Controllers.General;

namespace Views.Menu
{
    public class GameBtnView : MonoBehaviour
    {
        public event Action PressBtnAction;
        public event Action EndAnimAction;
        
        [SerializeField] 
        private VerticalAnimBetweenTwoPointsController _balloon;
        [SerializeField] 
        private Button _btn;

        private void OnEnable()
        {
            _btn.onClick.AddListener(OnPressBtn);
        }

        private void OnDisable()
        {
            _btn.onClick.RemoveAllListeners();
        }

        public void DeactivateBtn()
        {
            _btn.interactable = false;
        }

        private void OnPressBtn()
        {
            NotificationPressBtn();
            StartAnimBalloon();
        }

        private void StartAnimBalloon()
        {
            _balloon.StopAnim();

            RectTransform balloonRect = _balloon.gameObject.GetComponent<RectTransform>();

            float targetY = balloonRect.anchoredPosition.y + 400;

            balloonRect.DOAnchorPosY(targetY, 1).OnComplete(NotificationEndAnim);
        }

        private void NotificationPressBtn()
        {
            PressBtnAction?.Invoke();
        }

        private void NotificationEndAnim()
        {
            EndAnimAction?.Invoke();
        }
    }
}