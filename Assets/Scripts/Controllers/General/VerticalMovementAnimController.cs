using UnityEngine;
using DG.Tweening;

namespace Controllers.General
{
    public class VerticalMovementAnimController : AbstractAnimController
    {
        [SerializeField] 
        private float _delayBetweenLoops;

        private float _halfImageHeight;
        private float _targetY;
        private float _startY;
        
        protected override void Initialize()
        {
            
        }

        protected override void OnSceneEnable()
        {
            
        }

        protected override void OnSceneDisable()
        {
            
        }

        public void SetCanvasWidth(float halfHeight)
        {
            _halfImageHeight = base.Rect.rect.height /2;

            _targetY = halfHeight + _halfImageHeight;
            _startY = -halfHeight - _halfImageHeight;
        }

        public void StartMoving()
        {
            if (base.AnimTween != null)
            {
                base.AnimTween.Kill();
            }
            
            float distanceToEnd = Mathf.Abs(_targetY - base.Rect.anchoredPosition.y);
            float animationDuration = distanceToEnd / base.Speed;

            base.AnimTween = base.Rect.DOAnchorPosY(_targetY, animationDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    base.Rect.anchoredPosition = new Vector2(base.Rect.anchoredPosition.x, _startY);
                    DOVirtual.DelayedCall(_delayBetweenLoops, StartMoving);
                });
        }
    }
}
