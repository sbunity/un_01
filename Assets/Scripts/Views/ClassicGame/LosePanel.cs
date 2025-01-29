using System;
using UnityEngine;

using DG.Tweening;
using Views.General;

namespace Views.ClassicGame
{
    public class LosePanel : PanelView
    {
        public event Action<int> AnimComplitedAction;

        [SerializeField] 
        private RectTransform _baloonRect;

        private void Start()
        {
            base.PressBtnAction += OnPressBtn;
        }

        private void OnDestroy()
        {
            base.PressBtnAction -= OnPressBtn;
        }

        private void OnPressBtn(int index)
        {
            StartBaloonAnim(index);
        }

        private void StartBaloonAnim(int index)
        {
            _baloonRect.DOAnchorPosY(_baloonRect.anchoredPosition.y + 400, 1).OnComplete(() => Notification(index));
        }

        private void Notification(int index)
        {
            AnimComplitedAction?.Invoke(index);
        }
    }
}