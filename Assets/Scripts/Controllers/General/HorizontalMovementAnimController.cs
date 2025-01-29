using UnityEngine;
using DG.Tweening;

namespace Controllers.General
{
    public class HorizontalMovementAnimController : AbstractAnimController
    {
        [SerializeField] 
        private float _delayBetweenLoops;

        private float _halfCloudWidth;
        private float _targetX;
        private float _startX;

        protected override void Initialize()
        {
            
        }

        protected override void OnSceneEnable()
        {

        }

        protected override void OnSceneDisable()
        {
        }

        public void SetCanvasWidth(float halfWidth)
        {
            _halfCloudWidth = base.Rect.rect.width /2;

            _targetX = -halfWidth - _halfCloudWidth;
            _startX = halfWidth + _halfCloudWidth;
        }

        public void StartMoving()
        {
            if (base.AnimTween != null)
            {
                base.AnimTween.Kill();
            }
            
            float distanceToEnd = Mathf.Abs(base.Rect.localPosition.x - _targetX);
            float animationDuration = distanceToEnd / base.Speed;

            base.AnimTween = base.Rect.DOLocalMoveX(_targetX, animationDuration)
                .SetEase(Ease.Linear)
                .OnComplete(() =>
                {
                    base.Rect.localPosition = new Vector2(_startX, base.Rect.localPosition.y);
                    DOVirtual.DelayedCall(_delayBetweenLoops, StartMoving);
                });
        }
    }
}