using System;
using System.Globalization;

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Views.ClassicGame
{
    public class BaloonScallerView : MonoBehaviour
    {
        public event Action StartPressAction;
        public event Action<BaloonScallerView, float> PausePressAction;
        public event Action<BaloonScallerView> EndPressAction;
        public event Action StartExplodeAnimAction;
        
        [SerializeField] 
        private Text _coefficientCountText;
        [SerializeField] 
        private Image _mainImage;
        [SerializeField] 
        private Image _splatterImage;
        
        [SerializeField] 
        private float _scaleMultiplier = 1.5f;
        [SerializeField] 
        private float _scaleDuration = 0.5f;

        private RectTransform _rect;
        
        private Tween scaleTween;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        public void SetTargetScale(float value)
        {
            _scaleMultiplier = value;
        }

        public void OnPointerDown()
        {
            if (_rect.localScale.x == _scaleMultiplier)
            {
                return;
            }

            scaleTween?.Kill();
            
            StartPressAction?.Invoke();

            float speed = (_scaleMultiplier - _rect.localScale.x) * _scaleDuration;

            scaleTween = _rect.DOScale(_scaleMultiplier, speed)
                .SetEase(Ease.Linear).OnUpdate(UpdateBetCount).OnComplete(NotificationEndPress);;
        }
        
        public void OnPointerUp()
        {
            scaleTween?.Kill();
            
            if (_rect.localScale.x == _scaleMultiplier)
            {
                return;
            }

            if (Math.Abs(_rect.localScale.x - _scaleMultiplier) > 0)
            {
                PausePressAction?.Invoke(this, _rect.localScale.x);
            }
        }

        private void NotificationEndPress()
        {
            StartExplodeAnimAction?.Invoke();
            
            ExplodeAnim();
        }

        private void UpdateBetCount()
        {
            _coefficientCountText.text = _rect.localScale.x.ToString("F1", CultureInfo.InvariantCulture) +"X";
        }

        private void ExplodeAnim()
        {
            _coefficientCountText.gameObject.SetActive(false);
            
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
                EndPressAction?.Invoke(this);
            });
        }
    }
}