using System;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Views.CollectionGame
{
    public class BaloonView : MonoBehaviour
    {
        public event Action<BaloonView> EndMoveAction;
        public event Action<BaloonView> PressAction;

        [SerializeField] 
        private Image _mainImage;
        [SerializeField]
        private Image _iconImage;
        [SerializeField] 
        private Image _splatterImage;
        [SerializeField] 
        private float _speed;

        private Tween _moveTween;

        public void SetSprite(Sprite sprite)
        {
            _mainImage.sprite = sprite;
        }

        public void SetIcon(Sprite sprite)
        {
            if (sprite == null)
            {
                _iconImage.enabled = false;
            }

            _iconImage.sprite = sprite;
        }

        public void SetSplatter(Sprite sprite)
        {
            _splatterImage.sprite = sprite;
        }

        public void SetSize(float size)
        {
            _mainImage.rectTransform.localScale = new Vector2(size, size);
        }

        public void Move()
        {
            _moveTween = _mainImage.rectTransform.DOAnchorPosY(_mainImage.rectTransform.anchoredPosition.y + 600, _speed)
                .SetEase(Ease.Linear)
                .OnComplete(OnEndMove);
        }
        
        public void OnPointerDown()
        {
            _moveTween?.Kill();

            ExplodeAnim();
        }
        
        private void ExplodeAnim()
        {
            Sequence explosionSequence = DOTween.Sequence();
            
            explosionSequence.Append(_mainImage.DOFade(0, 0.1f)
                .SetEase(Ease.OutQuad));
            
            explosionSequence.AppendCallback(() =>
            {
                _mainImage.enabled = false;
                _splatterImage.gameObject.SetActive(true);
                _splatterImage.GetComponent<CanvasGroup>().alpha = 0;
            });
            
            explosionSequence.Append(_splatterImage.DOFade(1, 0.3f)
                .SetEase(Ease.OutQuad));
            
            explosionSequence.Append(_splatterImage.DOFade(0, 1.0f)
                .SetEase(Ease.InQuad));
            
            explosionSequence.OnComplete(() =>
            {
                _iconImage.enabled = false;
                PressAction?.Invoke(this);
            });
        }

        private void OnEndMove()
        {
            EndMoveAction?.Invoke(this);
        }
    }
}