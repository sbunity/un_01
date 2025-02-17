using System;
using System.Collections;
using UnityEngine;

namespace Prototype.SceneLoaderCore.Helpers
{
    public class UIFader : MonoBehaviour
    {
        public delegate void  CompleteFadeEvent(Action callback);
        public static CompleteFadeEvent OnCompleteFade;
        
        public static UIFader Instance;

        [Header("UI element(canvas group)")]
        private CanvasGroup uiElement;

        [Header("Fade time")]
        private float lerpTime = .5f;

        private void Awake()
        {
            Instance = this;
            uiElement = GetComponentInChildren<CanvasGroup>();
        }

        internal void FadeIn()
        {
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 1, lerpTime));
        }
        
        internal void FadeOut()
        {
            StartCoroutine(FadeCanvasGroup(uiElement, uiElement.alpha, 0, lerpTime));
        }
        
        private IEnumerator FadeCanvasGroup(CanvasGroup cg, float start, float end, float lerpTime = 1)
        {
            var timeStartedLerping = Time.time;
            var timeSinceStarted = Time.time - timeStartedLerping;
            var percentageComplete = timeSinceStarted / lerpTime;

            while (true)
            {
                timeSinceStarted = Time.time - timeStartedLerping;
                percentageComplete = timeSinceStarted / lerpTime;

                var currentValue = Mathf.Lerp(start, end, percentageComplete);

                cg.alpha = currentValue;

                if (percentageComplete >= 1) break;
                yield return new WaitForFixedUpdate();
            }

            CompleteFade(end);
        }

        private void CompleteFade(float end)
        {
            if (end != 1) return;
            
            if (OnCompleteFade != null)
            {
                OnCompleteFade.Invoke(CallBack);
            }
            else
            {
                CallBack();
            }
        }

        private void CallBack()
        {
            SceneLoader.Instance.LoadScene();
        }
    }
}
