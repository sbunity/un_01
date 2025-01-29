using UnityEngine;
using DG.Tweening;

namespace Controllers.General
{
    public class VerticalAnimBetweenTwoPointsController : AbstractAnimController
    {
        [SerializeField] 
        private float _distanceMovement;
        [SerializeField] 
        private bool _isFirstTop;
        
        private Vector2 _startPos;
        private float _topTargetY;
        private float _bottomTargetY;

        protected override void Initialize()
        {
            _startPos = base.Rect.anchoredPosition;

            _topTargetY = _startPos.y + _distanceMovement;
            _bottomTargetY = _startPos.y - _distanceMovement;
        }

        protected override void OnSceneEnable()
        {
            StartMove();
        }

        protected override void OnSceneDisable()
        {
            
        }

        public void StopAnim()
        {
            base.AnimTween.Kill();
        }

        private void StartMove()
        {
            if (_isFirstTop)
            {
                MoveToTop();
            }
            else
            {
                MoveToBottom();
            }
        }

        private void MoveToTop()
        {
            if (base.AnimTween != null)
            {
                base.AnimTween.Kill();
            }

            base.AnimTween = base.Rect.DOAnchorPosY(_topTargetY, base.Speed)
                .SetEase(Ease.Linear)
                .OnComplete(MoveToBottom);
        }

        private void MoveToBottom()
        {
            if (base.AnimTween != null)
            {
                base.AnimTween.Kill();
            }
            
            base.AnimTween = base.Rect.DOAnchorPosY(_bottomTargetY, base.Speed)
                .SetEase(Ease.Linear)
                .OnComplete(MoveToTop);
        }
    }
}