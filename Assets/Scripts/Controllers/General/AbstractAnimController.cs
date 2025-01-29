using UnityEngine;
using DG.Tweening;

namespace Controllers.General
{
    public abstract class AbstractAnimController : MonoBehaviour
    {
        [SerializeField] 
        private float _speed;
        [SerializeField]
        private RectTransform _rect; 
            
        private Tween _animTween;

        protected float Speed => _speed;
        protected RectTransform Rect => _rect;

        protected Tween AnimTween
        {
            get => _animTween;
            set => _animTween = value;
        }

        private void Awake()
        {
            if (_rect == null)
            {
                _rect = GetComponent<RectTransform>();
            }

            Initialize();
        }

        private void OnEnable()
        {
            OnSceneEnable();
        }

        private void OnDisable()
        {
            _animTween.Kill();
            
            OnSceneDisable();
        }

        protected abstract void Initialize();
        protected abstract void OnSceneEnable();
        protected abstract void OnSceneDisable();
    }
}