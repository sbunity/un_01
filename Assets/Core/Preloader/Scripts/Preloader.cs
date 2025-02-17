using UnityEngine;
using DG.Tweening;
using Prototype.SceneLoaderCore.Helpers;

namespace Core
{
    public class Preloader : MonoBehaviour
    {
        [SerializeField, Header("Panel")] protected GameObject panel;
        
        [SerializeField, Header("Progress")] protected Transform progress;

        protected Tween _tween;

        private void OnEnable() => Play();
        
        private void OnDisable()
        {
            if (_tween == null) return;
            
            _tween.Kill();

            _tween = null;
        }

        protected virtual  void Play()
        {
           
        }

        public void Show()
        {
            if(panel.activeSelf) return;
            
            panel.SetActive(true);

            if (_tween != null)
                _tween.Play();
            else 
                Play();
        }

        private void Hide()
        {
            panel.SetActive(false);

            StopAnimation();
        }
        
        public void WVLoadingCompleted()
        {
            Hide();
        }
        
        public void DefaultB1n0mLoadingCompleted()
        {
            if(SceneLoader.Instance)
                SceneLoader.Instance.SwitchToScene(SceneLoader.Instance.mainScene);
        }
        
        private void StopAnimation()
        {
            if (_tween == null) return;

            if (!_tween.IsPlaying()) return;

            _tween.Pause();
        }
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.H))
            {
                Hide();
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                Show();
            }
        }
    }
}